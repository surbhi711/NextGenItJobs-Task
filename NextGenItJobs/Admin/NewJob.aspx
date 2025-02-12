<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NewJob.aspx.cs" Inherits="NextGenItJobs.Admin.NewJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-12 grid-margin">
        <div class="card">
            <div class="card-body">
                <div class="text-center mb-3">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <h4 class="card-title mb-4">Add New Job</h4>
                <div class="form-sample">
                    <%--<form class="form-sample">--%>
                    <%--<p class="card-description">Personal info </p>--%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtJobTitle" class="form-label">Job Title</label>
                                <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" placeholder="Ex. Web Developer,App Developer" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtNoOfPost" class="form-label">Number Of Post</label>
                                <asp:TextBox ID="txtNoOfPost" runat="server" CssClass="form-control" placeholder="Enter Number of Position" TextMode="Number" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <%--<div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">Gender</label>
                                <div class="col-sm-9">
                                    <select class="form-select">
                                        <option>Male</option>
                                        <option>Female</option>
                                    </select>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtDescription" class="form-label">Description</label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Job Description" TextMode="MultiLine" required></asp:TextBox>
                                <%--<input class="form-control" placeholder="dd/mm/yyyy" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtQualification" class="form-label">Qualification/Education Required</label>
                                <asp:TextBox ID="txtQualification" runat="server" CssClass="form-control" placeholder="Ex. MCA, B.ScIT, BTech, MBA" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtExperience" class="form-label">Experience Required</label>
                                <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Ex: 2 Years, 1.5 Years" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSpecialization" class="form-label">Specialization Required</label>
                                <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" placeholder="Enter Specialization" TextMode="MultiLine" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtLastDate" class="form-label">Last Date To Apply</label>
                                <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" placeholder="Enter Last Date To Apply" TextMode="Date" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSalary" class="form-label">Salary</label>
                                <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" placeholder="Ex: 25000/Month, 7L/Year" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="ddlJobType" class="form-label">Job Type</label>
                                <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-select form-control">
                                    <asp:ListItem Value="0">Select Job Type</asp:ListItem>
                                    <asp:ListItem>Full Time</asp:ListItem>
                                    <asp:ListItem>Part Time</asp:ListItem>
                                    <asp:ListItem>Remote</asp:ListItem>
                                    <asp:ListItem>Freelance</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="JobType is Required" ForeColor="Red" ControlToValidate="ddlJobType" InitialValue="0" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtCompany" class="form-label">Company/Organization Name</label>
                                <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" placeholder="Enter Company/Organization Name" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="ddlJobType" class="form-label">Company/Organization Logo</label>
                                <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" ToolTip=".jpg, .jpeg, .png extension only" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtWebsite" class="form-label">Website</label>
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Enter Website" TextMode="Url"></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtAddress" class="form-label">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Work Location" TextMode="MultiLine" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="ddlCountry" class="form-label">Country</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control form-select" AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName">
                                    <asp:ListItem Value="0">Select Country</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Country is Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [CountryName] FROM [Country]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtState" class="form-label">State</label>
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="Enter State" required></asp:TextBox>
                                <%--<input type="text" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <%--</form>--%>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-gradient-primary" BackColor="#7200cf" Text="Add Job" OnClick="btnAdd_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
