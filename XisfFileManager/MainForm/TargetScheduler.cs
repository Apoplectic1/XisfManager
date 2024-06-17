using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XisfFileManager.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace XisfFileManager
{
    public partial class MainForm
    {
        // **********************************************************************************************************************************
        // **********************************************************************************************************************************
        // Target Scheduler Methods
        // **********************************************************************************************************************************
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


        private string ProfilePreference(TreeNode projectNode) { return mSchedulerDB.mProfilePreferenceList.Find(profile => profile.profileId.Contains(projectNode.Parent.Text)).profileId; }
        private bool ProjectState(TreeNode projectNode, string sProfilePreference) { return mSchedulerDB.mProjectList.Any(project => project.name == projectNode.Text && project.profileId == sProfilePreference); }
        private int ProjectPriority(TreeNode projectNode, string sProfilePreference) { return mSchedulerDB.mProjectList.Find(project => (project.name == projectNode.Text) && (project.profileId == sProfilePreference)).priority; }


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
            TreeNode profileNode = clickedNode.Parent;

            // Clear existing nodes
            TreeView_SchedulerTab_TargetTree.Nodes.Clear();

            // Retrieve the profile ID based on the profile node text
            string projectProfileId = mSchedulerDB.mProjectList
                .FirstOrDefault(project => project.profileId.Contains(profileNode.Text))?.profileId;

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



        // **********************************************************************************************************************************
        // Event Handlers
        // **********************************************************************************************************************************

        /// <summary>
        /// Handles the NodeMouseClick event for the ProfileTree in the Scheduler tab.
        /// Displays a message box with the text of the clicked node.
        /// </summary>
        private void TreeView_SchedulerTab_ProfileTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode clickedNode = e.Node;
            string clickedItem = clickedNode.Text;
            MessageBox.Show($"You clicked on: {clickedItem}");
        }

        /// <summary>
        /// Handles the NodeMouseClick event for the ProjectTree in the Scheduler tab.
        /// Depending on the node clicked (Profile or Project), it refines the project tree, sets project active status, priority, and refines the target tree and exposure plans.
        /// </summary>
        private void TreeView_SchedulerTab_ProjectTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode clickedNode = e.Node;

            if (clickedNode.Parent == null)
            {
                // We clicked on a Profile
                RefineSelectedProjectTreeView(clickedNode);
            }
            else
            {
                // We clicked on a Project
                SetProjectActiveCheckBox(clickedNode);
                SetProjectPriorityRadioButtons(clickedNode);
                RefineSelectedTargetTreeView(clickedNode);
            }

            RefineExposurePlans();
        }

        /// <summary>
        /// Handles the NodeMouseClick event for the PlanTree in the Scheduler tab.
        /// Displays a message box with the text of the clicked node.
        /// </summary>
        private void TreeView_SchedulerTab_PlanTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode clickedNode = e.Node;
            if (clickedNode == null)
                return;

            string clickedItem = clickedNode.Text;
            MessageBox.Show($"You clicked on: {clickedItem}");
        }

        /// <summary>
        /// Handles the NodeMouseClick event for the TargetTree in the Scheduler tab.
        /// Refines the exposure plans based on the clicked node.
        /// </summary>
        private void TreeView_SchedulerTab_TargetTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode clickedNode = e.Node;
            if (clickedNode == null)
                return;

            RefineExposurePlans();
        }

        /// <summary>
        /// Placeholder for handling click events on the ProjectTree in the Scheduler tab.
        /// Currently does nothing.
        /// </summary>
        private void TreeView_SchedulerTab_ProjectTree_Click(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// Custom draw logic for nodes in the ProjectTree in the Scheduler tab.
        /// Draws nodes with different colors based on project priority (LOW, NORMAL, HIGH).
        /// </summary>
        private void TreeView_SchedulerTab_ProjectTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e == null)
                return;

            if (e.Node.Parent == null)
            {
                e.DrawDefault = true;
                return;
            }

            string profilePreference = ProfilePreference(e.Node);
            int priority = ProjectPriority(e.Node, profilePreference);

            switch (priority)
            {
                case (int)eProjectPriority.LOW:
                    e.Graphics.DrawString(e.Node.Text, DefaultFont, new SolidBrush(Color.SandyBrown), new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                    break;
                case (int)eProjectPriority.NORMAL:
                    e.Graphics.DrawString(e.Node.Text, DefaultFont, Brushes.Black, e.Bounds);
                    break;
                case (int)eProjectPriority.HIGH:
                    e.Graphics.DrawString(e.Node.Text, DefaultFont, Brushes.DarkMagenta, e.Bounds);
                    break;
            }
        }


        // ######################################################################################################################################
        // ######################################################################################################################################

        public class CustomTreeView : System.Windows.Forms.TreeView
        {
            public CustomTreeView()
            {
                this.DrawMode = TreeViewDrawMode.OwnerDrawText;
                this.DrawNode += CustomTreeView_DrawNode;
            }

            /// <summary>
            /// Custom draw logic for nodes in the TreeView.
            /// Draws node text and numeric up/down representations.
            /// </summary>
            private void CustomTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
            {
                // Prevent default drawing
                e.DrawDefault = false;

                // Draw the node text
                e.Graphics.DrawString(e.Node.Text, this.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top);

                // Calculate positions for numeric up/down representations
                int spacing = 5; // Spacing between text and numeric controls
                Size textSize = TextRenderer.MeasureText(e.Node.Text, this.Font);
                Point numericControlStartPoint = new Point(e.Bounds.Left + textSize.Width + spacing, e.Bounds.Top);
                Size numericControlSize = new Size(20, e.Bounds.Height);

                Rectangle numericUpRect = new Rectangle(numericControlStartPoint, numericControlSize);
                Rectangle numericDownRect = new Rectangle(new Point(numericUpRect.Right + spacing, e.Bounds.Top), numericControlSize);

                // Draw numeric up/down representations
                using (Pen controlPen = new Pen(Color.Black, 1))
                using (Brush controlBrush = new SolidBrush(Color.Black))
                {
                    // Numeric Up Control
                    e.Graphics.DrawRectangle(controlPen, numericUpRect);
                    e.Graphics.DrawString("+", this.Font, controlBrush, numericUpRect.X + 5, numericUpRect.Y + 2);

                    // Numeric Down Control
                    e.Graphics.DrawRectangle(controlPen, numericDownRect);
                    e.Graphics.DrawString("-", this.Font, controlBrush, numericDownRect.X + 5, numericDownRect.Y + 2);
                }
            }


            /// <summary>
            /// Handles the MouseDown event for the TreeView, providing custom click logic for numeric up/down controls.
            /// </summary>
            /// <param name="e">Mouse event arguments</param>
            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);

                // Get the node at the mouse click location
                TreeNode nodeAtClick = this.GetNodeAt(e.X, e.Y);
                if (nodeAtClick != null)
                {
                    // Calculate the text size and bounds for the numeric up/down controls
                    Size textSize = TextRenderer.MeasureText(nodeAtClick.Text, this.Font);
                    int spacing = 5;
                    Rectangle nodeBounds = nodeAtClick.Bounds;
                    Rectangle numericUpRect = new Rectangle(nodeBounds.Left + textSize.Width + spacing, nodeBounds.Top, 20, nodeBounds.Height);
                    Rectangle numericDownRect = new Rectangle(numericUpRect.Right + spacing, nodeBounds.Top, 20, nodeBounds.Height);

                    // Check if the click was within the numeric up control bounds
                    if (numericUpRect.Contains(e.Location))
                    {
                        MessageBox.Show("Increment value for " + nodeAtClick.Text);
                    }
                    // Check if the click was within the numeric down control bounds
                    else if (numericDownRect.Contains(e.Location))
                    {
                        MessageBox.Show("Decrement value for " + nodeAtClick.Text);
                    }
                }
            }
        }
    }
}