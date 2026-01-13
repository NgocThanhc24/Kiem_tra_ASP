using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLBanSach.Models;

namespace QLBanSach
{
    public partial class QTSach : System.Web.UI.Page
    {
        SachDAO sachDAO = new SachDAO();
        List<Sach> dsSach;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDuLieu();
            }
        }

        private void LoadDuLieu()
        {
            dsSach = sachDAO.getAll();
            gvSach.DataSource = dsSach;
            gvSach.DataBind();
            lblThongBao.Text = "";
        }

        private void LoadDuLieuTheoTen(string tenSach)
        {
            dsSach = sachDAO.laySachTheoTen(tenSach);
            if (dsSach.Count == 0)
            {
                lblThongBao.Text = "Tìm kiếm không có kết quả nào";
                gvSach.DataSource = null;
            }
            else
            {
                lblThongBao.Text = "";
                gvSach.DataSource = dsSach;
            }
            gvSach.DataBind();
        }

        protected void btTraCuu_Click(object sender, EventArgs e)
        {
            string tenSach = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(tenSach))
            {
                LoadDuLieu();
            }
            else
            {
                LoadDuLieuTheoTen(tenSach);
            }
        }

        protected void gvSach_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSach.PageIndex = e.NewPageIndex;
            string tenSach = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(tenSach))
            {
                LoadDuLieu();
            }
            else
            {
                LoadDuLieuTheoTen(tenSach);
            }
        }

        protected void gvSach_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSach.EditIndex = e.NewEditIndex;
            string tenSach = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(tenSach))
            {
                LoadDuLieu();
            }
            else
            {
                LoadDuLieuTheoTen(tenSach);
            }
        }

        protected void gvSach_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSach.EditIndex = -1;
            string tenSach = txtTen.Text.Trim();
            if (string.IsNullOrEmpty(tenSach))
            {
                LoadDuLieu();
            }
            else
            {
                LoadDuLieuTheoTen(tenSach);
            }
        }

        protected void gvSach_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int maSach = Convert.ToInt32(gvSach.DataKeys[e.RowIndex].Value);
                
                TextBox txtTenSach = (TextBox)gvSach.Rows[e.RowIndex].FindControl("txtTenSach");
                TextBox txtDonGia = (TextBox)gvSach.Rows[e.RowIndex].FindControl("txtDonGia");
                CheckBox chkKhuyenMai = (CheckBox)gvSach.Rows[e.RowIndex].FindControl("chkKhuyenMai");

                Sach sach = new Sach
                {
                    MaSach = maSach,
                    TenSach = txtTenSach.Text.Trim(),
                    Dongia = Convert.ToInt32(txtDonGia.Text),
                    KhuyenMai = chkKhuyenMai.Checked
                };

                int result = sachDAO.Update(sach);
                if (result > 0)
                {
                    gvSach.EditIndex = -1;
                    string tenSach = txtTen.Text.Trim();
                    if (string.IsNullOrEmpty(tenSach))
                    {
                        LoadDuLieu();
                    }
                    else
                    {
                        LoadDuLieuTheoTen(tenSach);
                    }
                }
                else
                {
                    lblThongBao.Text = "Cập nhật thất bại!";
                }
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void gvSach_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int maSach = Convert.ToInt32(gvSach.DataKeys[e.RowIndex].Value);
                Sach sach = new Sach { MaSach = maSach };
                
                int result = sachDAO.Delete(sach);
                if (result > 0)
                {
                    string tenSach = txtTen.Text.Trim();
                    if (string.IsNullOrEmpty(tenSach))
                    {
                        LoadDuLieu();
                    }
                    else
                    {
                        LoadDuLieuTheoTen(tenSach);
                    }
                }
                else
                {
                    lblThongBao.Text = "Xóa thất bại!";
                }
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void gvSach_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Xử lý các command khác nếu cần
        }

        protected void gvSach_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            foreach (Control control in e.Row.Cells[e.Row.Cells.Count - 1].Controls)
            {
                if (control is LinkButton linkButton)
                {
                    ApplyCommandButtonStyle(linkButton, linkButton.CommandName);
                }
                else if (control is Button button)
                {
                    ApplyCommandButtonStyle(button, button.CommandName);
                }
            }
        }

        private static void ApplyCommandButtonStyle(WebControl control, string commandName)
        {
            control.CssClass = (control.CssClass + " btn btn-sm").Trim();

            switch (commandName)
            {
                case "Edit":
                    control.CssClass = (control.CssClass + " btn-primary").Trim();
                    break;
                case "Delete":
                    control.CssClass = (control.CssClass + " btn-danger").Trim();
                    break;
                case "Update":
                    control.CssClass = (control.CssClass + " btn-success").Trim();
                    break;
                case "Cancel":
                    control.CssClass = (control.CssClass + " btn-secondary").Trim();
                    break;
            }
        }
    }
}