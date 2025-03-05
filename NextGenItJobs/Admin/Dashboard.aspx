<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="NextGenItJobs.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-4 stretch-card grid-margin">
            <div class="card bg-gradient-danger card-img-holder text-white">
                <div class="card-body">
                    <img src="../assetsa/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h2 class="font-weight-normal mb-3">Total Users</h2>
                    <%--<h2 class="mb-5">$ 15,0000</h2>--%>
                    <h4 class="mb-5"><% Response.Write(Session["Users"]); %></h4>
                </div>
            </div>
        </div>
        <div class="col-md-4 stretch-card grid-margin">
            <div class="card bg-gradient-info card-img-holder text-white">
                <div class="card-body">
                    <img src="../assetsa/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h2 class="font-weight-normal mb-3">Total Jobs</h2>
                    <%--<h2 class="mb-5">$ 15,0000</h2>--%>
                    <h4 class="mb-5"><% Response.Write(Session["Jobs"]); %></h4>
                </div>
            </div>
        </div>
        <div class="col-md-4 stretch-card grid-margin">
            <div class="card bg-gradient-success card-img-holder text-white">
                <div class="card-body">
                    <img src="../assetsa/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h2 class="font-weight-normal mb-3">Applied Jobs</h2>
                    <%--<h2 class="mb-5">$ 15,0000</h2>--%>
                    <h4 class="mb-5"><% Response.Write(Session["AppliedJobs"]); %></h4>
                </div>
            </div>
        </div>        
    </div>
    <div class="row">
        <div class="col-md-4 stretch-card grid-margin">
            <div class="card bg-gradient-success card-img-holder text-white">
                <div class="card-body">
                    <img src="../assetsa/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h2 class="font-weight-normal mb-3">Contacted Users</h2>
                    <%--<h2 class="mb-5">$ 15,0000</h2>--%>
                    <h4 class="mb-5"><% Response.Write(Session["Contact"]); %></h4>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
