using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using QLBanSach.Models;

namespace QLBanSach
{
    public partial class ThemSach : System.Web.UI.Page
    {
        private readonly ChuDeDAO chuDeDAO = new ChuDeDAO();
        private readonly SachDAO sachDAO = new SachDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlChuDe.DataSource = chuDeDAO.getAll();
                ddlChuDe.DataTextField = "TenCD";
                ddlChuDe.DataValueField = "MaCD";
                ddlChuDe.DataBind();
            }
        }

        protected void btXuLy_Click(object sender, EventArgs e)
        {
            string tenSach = txtTen.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenSach))
            {
                return;
            }

            if (!int.TryParse(txtDonGia.Text.Trim(), out int donGia))
            {
                return;
            }

            if (!int.TryParse(ddlChuDe.SelectedValue, out int maCD))
            {
                return;
            }

            string fileName = "";
            if (FHinh.HasFile)
            {
                fileName = Path.GetFileName(FHinh.FileName);
                string folderPath = Server.MapPath("~/Bia_sach/");
                Directory.CreateDirectory(folderPath);
                string savePath = Path.Combine(folderPath, fileName);
                FHinh.SaveAs(savePath);
            }

            var sach = new Sach
            {
                TenSach = tenSach,
                Dongia = donGia,
                MaCD = maCD,
                Hinh = fileName,
                KhuyenMai = chkKhuyenMai.Checked,
                NgayCapNhat = DateTime.Now
            };

            sachDAO.Insert(sach);

            Response.Redirect("QTSach.aspx");
        }
    }
}