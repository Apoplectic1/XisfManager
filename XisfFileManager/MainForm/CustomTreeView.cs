using System.Drawing;
using System.Windows.Forms;

namespace XisfFileManager
{
    /// <summary>
    /// A TreeView that owner-draws each node's text followed by simple +/- numeric
    /// up/down indicators and reports clicks on them. Hosts the Target Scheduler
    /// exposure-plan tree (see mExposureTreeView).
    /// </summary>
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
        private void CustomTreeView_DrawNode(object? sender, DrawTreeNodeEventArgs e)
        {
            // Prevent default drawing
            e.DrawDefault = false;

            // Draw the node text
            e.Graphics?.DrawString(e.Node?.Text, this.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top);

            // Calculate positions for numeric up/down representations
            int spacing = 5; // Spacing between text and numeric controls
            Size textSize = TextRenderer.MeasureText(e.Node?.Text, this.Font);
            Point numericControlStartPoint = new Point(e.Bounds.Left + textSize.Width + spacing, e.Bounds.Top);
            Size numericControlSize = new Size(20, e.Bounds.Height);

            Rectangle numericUpRect = new Rectangle(numericControlStartPoint, numericControlSize);
            Rectangle numericDownRect = new Rectangle(new Point(numericUpRect.Right + spacing, e.Bounds.Top), numericControlSize);

            // Draw numeric up/down representations
            using (Pen controlPen = new Pen(Color.Black, 1))
            using (Brush controlBrush = new SolidBrush(Color.Black))
            {
                // Numeric Up Control
                e.Graphics?.DrawRectangle(controlPen, numericUpRect);
                e.Graphics?.DrawString("+", this.Font, controlBrush, numericUpRect.X + 5, numericUpRect.Y + 2);

                // Numeric Down Control
                e.Graphics?.DrawRectangle(controlPen, numericDownRect);
                e.Graphics?.DrawString("-", this.Font, controlBrush, numericDownRect.X + 5, numericDownRect.Y + 2);
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
            TreeNode? nodeAtClick = this.GetNodeAt(e.X, e.Y);
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
