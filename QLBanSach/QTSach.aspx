<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="QTSach.aspx.cs" Inherits="QLBanSach.QTSach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NoiDung" runat="server">
    <style>
        .pager-yellow td {
            background: #fff9c4 !important;
            text-align: center;
            padding: 8px 0;
        }

        .pager-yellow td > table {
            margin: 0 auto;
        }

        .pager-yellow a,
        .pager-yellow span {
            display: inline-block;
            margin: 0 6px;
            padding: 2px 6px;
            text-decoration: none;
            font-weight: 600;
            color: #6f63ff;
        }

        #gvSach input[type="submit"][value="Sửa"],
        #gvSach input[type="button"][value="Sửa"] {
            background-color: #0d6efd;
            border-color: #0d6efd;
            color: #fff;
        }

        #gvSach input[type="submit"][value="Xóa"],
        #gvSach input[type="button"][value="Xóa"] {
            background-color: #dc3545;
            border-color: #dc3545;
            color: #fff;
        }
    </style>
     <h2>TRANG QUẢN TRỊ SÁCH</h2>
     <hr />   
     <div class="row mb-2">
         <div class="col-md-6">
              <div class="form-inline">
                   Tựa sách <asp:TextBox ID="txtTen" runat="server" placeholder="Nhập tựa sách cần tìm" CssClass="form-control ml-2" Width="300"></asp:TextBox>
                  <asp:Button ID="btTraCuu" runat="server" Text="Tra cứu" CssClass="btn btn-info ml-2" OnClick="btTraCuu_Click" />                 
              </div>
         </div>
        <div class="col-md-6 text-right">
             <asp:HyperLink ID="hlThemMoi" runat="server" NavigateUrl="ThemSach.aspx" CssClass="btn btn-success">Thêm sách mới</asp:HyperLink>
        </div>
    </div>
    
    <div class="row mt-3">
        <div class="col-12">
            <asp:Label ID="lblThongBao" runat="server" CssClass="text-danger" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div class="row mt-2">
        <div class="col-12">
            <asp:GridView ID="gvSach" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-bordered table-striped" AllowPaging="True" PageSize="4"
                OnPageIndexChanging="gvSach_PageIndexChanging" OnRowCommand="gvSach_RowCommand"
                OnRowEditing="gvSach_RowEditing" OnRowCancelingEdit="gvSach_RowCancelingEdit"
                OnRowUpdating="gvSach_RowUpdating" OnRowDeleting="gvSach_RowDeleting" OnRowDataBound="gvSach_RowDataBound"
                DataKeyNames="MaSach">
                <Columns>
                    <asp:BoundField DataField="MaSach" HeaderText="Mã sách" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Tựa sách">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTenSach" runat="server" Text='<%# Bind("TenSach") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTenSach" runat="server" Text='<%# Bind("TenSach") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ảnh bìa">
                        <ItemTemplate>
                            <img src='<%# "Bia_sach/" + Eval("Hinh") %>' alt="Ảnh bìa" style="width: 60px; height: 80px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Đơn giá">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDonGia" runat="server" Text='<%# Bind("Dongia") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDonGia" runat="server" Text='<%# Eval("Dongia", "{0:N0} VNĐ") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Khuyến mãi">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkKhuyenMai" runat="server" Checked='<%# Bind("KhuyenMai") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblKhuyenMai" runat="server" Text='<%# (bool)Eval("KhuyenMai") ? "x" : "" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Chọn thao tác" ShowEditButton="True" ShowDeleteButton="True" 
                        ButtonType="Button" ControlStyle-CssClass="btn btn-sm" 
                        EditText="Sửa" UpdateText="Lưu" CancelText="Hủy" DeleteText="Xóa">
                        <ControlStyle CssClass="btn btn-sm"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <PagerSettings Mode="Numeric" Position="Bottom" />
                <PagerStyle CssClass="pager-yellow" />
                <HeaderStyle CssClass="bg-danger text-white" />
            </asp:GridView>
        </div>
    </div>

</asp:Content>
