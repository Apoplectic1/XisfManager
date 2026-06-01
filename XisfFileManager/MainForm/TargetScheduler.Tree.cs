using System.Linq;
using System.Windows.Forms;
using XisfFileManager.Globals;

namespace XisfFileManager
{
    public partial class MainForm
    {
        // **********************************************************************************************************************************
        // Target Scheduler - database load, tree population and refinement
        // **********************************************************************************************************************************

        /// <summary>
        /// Handles the click event for the button that opens the Scheduler database, populates the profile, project, and target trees,
        /// and refines the exposure plans.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Button_SchedulerTab_OpenDatabase_Click(object sender, EventArgs e)
        {
            // Read the scheduler database file
            mSchedulerDB.mSqlReader.ReadTargetSchedulerDataBaseFile(@"\\BIRDWATCHER\SchedulerPlugin\schedulerdb.sqlite");

            // Clear existing nodes
            TreeView_SchedulerTab_ProfileTree.Nodes.Clear();
            TreeView_SchedulerTab_ProjectTree.Nodes.Clear();
            TreeView_SchedulerTab_TargetTree.Nodes.Clear();

            // Populate Profile Tree
            var profileNodes = mSchedulerDB.mProfilePreferenceList
                .Select(profilePreference => new TreeNode(profilePreference.profileId.Substring(profilePreference.profileId.LastIndexOf('-') + 5)))
                .ToArray();

            TreeView_SchedulerTab_ProfileTree.Nodes.AddRange(profileNodes);

            // Populate Project Tree
            var projectNodes = mSchedulerDB.mProjectList
                .Select(project => new { project.name, profileNode = TreeView_SchedulerTab_ProfileTree.Nodes.Cast<TreeNode>().FirstOrDefault(node => node.Text == project.profileId.Substring(project.profileId.LastIndexOf('-') + 5)) })
                .Where(p => p.profileNode != null)
                .Select(p => new TreeNode(p.name.Trim()))
                .ToArray();

            TreeView_SchedulerTab_ProjectTree.Nodes.AddRange(projectNodes);

            // Populate Target Tree
            var targetNodes = mSchedulerDB.mTargetList
                .Select(target => new TreeNode(target.name))
                .ToArray();

            TreeView_SchedulerTab_TargetTree.Nodes.AddRange(targetNodes);

            // Refine Exposure Plans
            RefineExposurePlans();

            // Expand all nodes in the Profile and Project Trees
            TreeView_SchedulerTab_ProfileTree.ExpandAll();
            TreeView_SchedulerTab_ProjectTree.ExpandAll();
        }


        private string ProfilePreference(TreeNode projectNode) { return mSchedulerDB.mProfilePreferenceList.Find(profile => profile.profileId.Contains(projectNode.Parent?.Text ?? string.Empty))?.profileId ?? string.Empty; }
        private bool ProjectState(TreeNode projectNode, string sProfilePreference) { return mSchedulerDB.mProjectList.Any(project => project.name == projectNode.Text && project.profileId == sProfilePreference); }
        private int ProjectPriority(TreeNode projectNode, string sProfilePreference) { return mSchedulerDB.mProjectList.Find(project => (project.name == projectNode.Text) && (project.profileId == sProfilePreference))?.priority ?? 0; }


        /// <summary>
        /// Sets the active status of the project checkbox based on the selected project node.
        /// </summary>
        /// <param name="projectNode">The selected project node.</param>
        private void SetProjectActiveCheckBox(TreeNode projectNode)
        {
            // Get the profile preference for the given project node
            string profilePreference = ProfilePreference(projectNode);

            // Set the active status of the checkbox based on the profile preference
            //CheckBox_ProjectActive.Checked = IsProjectActive(profilePreference);
        }


        /// <summary>
        /// Sets the project priority radio buttons based on the selected project node's priority.
        /// </summary>
        /// <param name="projectNode">The selected project node.</param>
        private void SetProjectPriorityRadioButtons(TreeNode projectNode)
        {
            // Get the profile preference and priority for the given project node
            string profilePreference = ProfilePreference(projectNode);
            int priority = ProjectPriority(projectNode, profilePreference);

            // Set the appropriate radio button based on the priority
            switch (priority)
            {
                case (int)eProjectPriority.LOW:
                    RadioButton_ProjectPriority_Low.Checked = true;
                    break;
                case (int)eProjectPriority.NORMAL:
                    RadioButton_ProjectPriority_Normal.Checked = true;
                    break;
                case (int)eProjectPriority.HIGH:
                    RadioButton_ProjectPriority_High.Checked = true;
                    break;
            }

            // Refine the selected project tree view
            RefineSelectedProjectTreeView(projectNode);
        }


        /// <summary>
        /// Recursively expands all nodes in the specified tree node collection.
        /// </summary>
        /// <param name="nodes">The collection of tree nodes to expand.</param>
        private static void ExpandAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                // Expand the current node
                node.Expand();

                // Recursively expand all child nodes
                ExpandAllNodes(node.Nodes);
            }
        }


        /// <summary>
        /// Refines the project tree view based on the selected node by clearing existing nodes and populating it with projects associated with the selected profile preference.
        /// </summary>
        /// <param name="clickedNode">The tree node that was clicked.</param>
        private void RefineSelectedProjectTreeView(TreeNode clickedNode)
        {
            // Clear existing nodes in the project and target tree views
            TreeView_SchedulerTab_ProjectTree.Nodes.Clear();
            TreeView_SchedulerTab_TargetTree.Nodes.Clear();

            // Get the profile preference based on the clicked node
            string profilePreference = ProfilePreference(clickedNode);

            // Create and add the root node to the project tree view
            TreeNode rootProjectNode = new TreeNode(profilePreference.Substring(profilePreference.LastIndexOf('-') + 5));
            TreeView_SchedulerTab_ProjectTree.Nodes.Add(rootProjectNode);

            // Filter projects based on the profile preference and add them as child nodes
            var projectNodes = mSchedulerDB.mProjectList
                .Where(project => project.profileId == profilePreference)
                .Select(project => new TreeNode(project.name))
                .ToArray();

            rootProjectNode.Nodes.AddRange(projectNodes);

            // Expand all nodes in the project tree view
            TreeView_SchedulerTab_ProjectTree.ExpandAll();
        }

        /// <summary>
        /// Refines the target tree view based on the clicked node.
        /// </summary>
        /// <param name="clickedNode">The node that was clicked.</param>
        private void RefineSelectedTargetTreeView(TreeNode clickedNode)
        {
            TreeNode? profileNode = clickedNode.Parent;

            // Clear existing nodes
            TreeView_SchedulerTab_TargetTree.Nodes.Clear();

            // Retrieve the profile ID based on the profile node text
            string? projectProfileId = mSchedulerDB.mProjectList
                .FirstOrDefault(project => project.profileId.Contains(profileNode?.Text ?? string.Empty))?.profileId;

            // Retrieve the project ID based on the clicked node text and profile ID
            int projectId = mSchedulerDB.mProjectList
                .FirstOrDefault(project => project.name == clickedNode.Text && project.profileId == projectProfileId)?.Id ?? -1;

            // Filter and add target nodes based on project ID and profile ID
            var targetNodes = mSchedulerDB.mTargetList
                .Where(target => target.projectid == projectId)
                .Join(mSchedulerDB.mProfilePreferenceList,
                      target => projectProfileId,
                      profilePreference => profilePreference.profileId,
                      (target, profilePreference) => target.name)
                .Distinct();

            foreach (var targetName in targetNodes)
            {
                TreeView_SchedulerTab_TargetTree.Nodes.Add(new TreeNode(targetName));
            }
        }


        /// <summary>
        /// Refines the exposure plans by associating them with their corresponding target nodes.
        /// </summary>
        private void RefineExposurePlans()
        {
            // Clear existing nodes in the exposure tree view
            mExposureTreeView.Nodes.Clear();

            // Iterate through each target node
            foreach (TreeNode targetNode in TreeView_SchedulerTab_TargetTree.Nodes)
            {
                string targetName = targetNode.Text;

                // Find the target ID based on the target node text
                int targetId = mSchedulerDB.mTargetList
                    .FirstOrDefault(target => target.name.Equals(targetName))?.Id ?? -1;

                if (targetId != -1)
                {
                    // Get a list of exposure template IDs for the target
                    var exposureTemplateIds = mSchedulerDB.mExposurePlanList
                        .Where(plan => plan.targetid == targetId)
                        .Select(plan => plan.exposureTemplateId)
                        .Distinct()
                        .ToList();

                    // Create and add tree nodes for each exposure plan
                    var exposurePlanNodes = exposureTemplateIds
                        .Select(planId => new
                        {
                            PlanId = planId,
                            FilterName = mSchedulerDB.mExposureTemplateList
                                .FirstOrDefault(template => template.Id == planId)?.filtername
                        })
                        .Where(plan => !string.IsNullOrEmpty(plan.FilterName))
                        .Select(plan => new TreeNode($"{targetName} {plan.FilterName}"))
                        .ToList();

                    mExposureTreeView.Nodes.AddRange(exposurePlanNodes.ToArray());
                }
            }
        }
    }
}
