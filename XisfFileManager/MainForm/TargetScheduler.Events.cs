using System.Drawing;
using System.Windows.Forms;
using XisfFileManager.Globals;

namespace XisfFileManager
{
    public partial class MainForm
    {
        // **********************************************************************************************************************************
        // Target Scheduler - tree event handlers
        // **********************************************************************************************************************************

        /// <summary>
        /// Handles the NodeMouseClick event for the ProfileTree in the Scheduler tab.
        /// Displays a message box with the text of the clicked node.
        /// </summary>
        private void TreeView_SchedulerTab_ProfileTree_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode? clickedNode = e.Node;
            if (clickedNode == null) return;
            string clickedItem = clickedNode.Text;
            MessageBox.Show($"You clicked on: {clickedItem}");
        }

        /// <summary>
        /// Handles the NodeMouseClick event for the ProjectTree in the Scheduler tab.
        /// Depending on the node clicked (Profile or Project), it refines the project tree, sets project active status, priority, and refines the target tree and exposure plans.
        /// </summary>
        private void TreeView_SchedulerTab_ProjectTree_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode? clickedNode = e.Node;
            if (clickedNode == null) return;

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
        private void TreeView_SchedulerTab_PlanTree_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode? clickedNode = e.Node;
            if (clickedNode == null)
                return;

            string clickedItem = clickedNode.Text;
            MessageBox.Show($"You clicked on: {clickedItem}");
        }

        /// <summary>
        /// Handles the NodeMouseClick event for the TargetTree in the Scheduler tab.
        /// Refines the exposure plans based on the clicked node.
        /// </summary>
        private void TreeView_SchedulerTab_TargetTree_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode? clickedNode = e.Node;
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
        private void TreeView_SchedulerTab_ProjectTree_DrawNode(object? sender, DrawTreeNodeEventArgs e)
        {
            if (e == null)
                return;

            if (e.Node?.Parent == null)
            {
                e.DrawDefault = true;
                return;
            }

            string profilePreference = ProfilePreference(e.Node);
            int priority = ProjectPriority(e.Node, profilePreference);

            switch (priority)
            {
                case (int)eProjectPriority.LOW:
                    e.Graphics?.DrawString(e.Node.Text, DefaultFont, new SolidBrush(Color.SandyBrown), new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                    break;
                case (int)eProjectPriority.NORMAL:
                    e.Graphics?.DrawString(e.Node.Text, DefaultFont, Brushes.Black, e.Bounds);
                    break;
                case (int)eProjectPriority.HIGH:
                    e.Graphics?.DrawString(e.Node.Text, DefaultFont, Brushes.DarkMagenta, e.Bounds);
                    break;
            }
        }
    }
}
