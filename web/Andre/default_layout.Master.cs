using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Andre
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["selProducts"] == null)
            {
                Session["selProducts"] = new ArrayList();
            }

          
        }

        internal static GridViewRow cloneRow(GridViewRow toClone)
        {
            GridViewRow clonedRow = new GridViewRow(toClone.RowIndex, toClone.DataItemIndex, toClone.RowType, toClone.RowState);
            TableCell[] clonedCells = new TableCell[9];

            for (int i = 0; i < 9; i++)
            {
                clonedCells[i] = new TableCell();
                clonedCells[i].Text = toClone.Cells[i].Text;
            }

            clonedRow.Cells.AddRange(clonedCells);
            return clonedRow;
        }
    }
}