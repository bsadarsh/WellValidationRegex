<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUpdateStart.aspx.cs"
    Inherits="DPRTDValidation.WebUpdateStart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Untitled Document</title>
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
<!--
select {
	width: 90%;
}
#OperatorListScroller {
	font-size: 95%;
	color: #000000;
	position: static;
	visibility: visible;
	overflow: auto;
	height: 450px;
}
#WebReportScroller {
	font-size: 95%;
	color: #000000;
	position: static;
	visibility: visible;
	border: 1px solid #333333;
	overflow: auto;
	height: 350px;
	width: 400px;
}
.ReportTable {
	border: 1px solid #86A7D2;
	background-color: #DAE4F3;
}
.ReportTitle {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: bold;
	color: #335B97;
	background-color: #AEC4E3;
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #86A7D2;
}
.ReportTableInTable {
	background-color: #F8FAFC;
	border: 1px solid #4D6CB7;
}
.ReportSubTitle {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: bold;
	color: #335B97;
	background-color: #CBDAED;
	border-top: 1px solid #86A7D2;
}
.ReportData {
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #BFD1E8;
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: normal;
	color: #00328B;
	text-decoration: none;
	background-color: #EEF3FB;
}
.ReportSubTotal {
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #86A7D2;
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: normal;
	color: #003366;
	text-decoration: none;
}
#divWebUsers {
	font-size: 95%;
	color: #000000;
	position: static;
	visibility: visible;
	overflow: auto;
	height: 500px;
}
#OperatorLogin {
	background-color: #E2B9A9;
	position: absolute;
	visibility: visible;
	width: 400px;
	left: 130px;
	top: 300px;
	padding: 5px;
	display: none;
}
-->
</style>
    <script language="JavaScript" src="Scripts/functions.js"></script>
    <script language="JavaScript" src="Scripts/ajax.js"></script>
    <script type="text/javascript">

        function redoARChecks() {
            frm.action = 'WebUpdateStart.asp';
            frm.Mode.value = 'ARCheck';
            frm.submit();
        }

        function continueWebUsers() {
            parent.window.setStatus('WebUsers', false, 'FTPCharts', true);
            document.getElementById('WebUsersTable').style.display = 'none';
        }

        function continueBackup() {
            parent.window.setStatus('NoChange', false, 'DTSPreLive', false);
            //entered 2 lines to set hidden fields again whose values got lost after the clicking the button
            //have you FTP'd the charts	- KJ16Dec04
            frm.Review.value = parent.window.document.frm.Review.value;
            frm.Year.value = 2000 + parseInt(parent.window.document.frm.Year.value, 10);
            //end insert lines

            frm.action = 'WebUpdateStart.asp';
            frm.Mode.value = 'SendStopMessage';
            frm.submit();
        }
        function continueAR(dispWarning) {
            var bOK = true;

            if (dispWarning) {
                if (!confirm('If you are sure you want to ignore the inconsistencies listed\nclick the OK button, otherwise click the Cancel button.')) {
                    bOK = false;
                }
            }

            if (bOK) {
                frm.action = 'WebUpdateStart.asp';
                frm.Mode.value = 'ARCalculate';
                frm.submit();
            }
        }

        function continueDuplicateWells() {
            parent.window.setStatus('DuplicateWells', false, 'WebUsers', false);
            document.getElementById('divDuplicateWells').style.display = 'none';

            frm.action = 'WebUpdateStart.asp';
            frm.Mode.value = 'WebUsers';
            frm.submit();
        }

        function addAllOperators() {
            var MasterList = document.frm.MasterList;
            var Operators = document.frm.Operators;
            var OperatorID;
            var OperatorName;

            if (MasterList.length != 0) {
                for (var n = 0; n < MasterList.length; n++) {
                    OperatorID = MasterList.options[n].value;
                    OperatorName = MasterList.options[n].text;
                    Operators.options[Operators.length] = new Option(OperatorName, OperatorID);
                }
                while (MasterList.length != 0)
                    MasterList.remove(0);
            }
        }

        function addSelectedOperators() {
            var MasterList = document.frm.MasterList;
            var Operators = document.frm.Operators;
            var OperatorID;
            var OperatorName;

            if (MasterList.length != 0) {
                for (var n = 0; n < MasterList.length; n++) {
                    if (MasterList.options[n].selected) {
                        OperatorID = MasterList.options[n].value;
                        OperatorName = MasterList.options[n].text;
                        Operators.options[Operators.length] = new Option(OperatorName, OperatorID);
                    }
                }
                for (var n = MasterList.length - 1; n >= 0; n--)
                    if (MasterList.options[n].selected)
                        MasterList.remove(n);
            }
        }

        function removeAllOperators() {
            var MasterList = document.frm.MasterList;
            var Operators = document.frm.Operators;
            var OperatorID;
            var OperatorName;

            if (Operators.length != 0) {
                for (var n = 0; n < Operators.length; n++) {
                    OperatorID = Operators.options[n].value;
                    OperatorName = Operators.options[n].text;
                    MasterList.options[MasterList.length] = new Option(OperatorName, OperatorID);
                }
                while (Operators.length != 0)
                    Operators.remove(0);
            }
        }

        function removeSelectedOperators() {
            var MasterList = document.frm.MasterList;
            var Operators = document.frm.Operators;
            var OperatorID;
            var OperatorName;

            if (Operators.length != 0) {
                for (var n = 0; n < Operators.length; n++) {
                    if (Operators.options[n].selected) {
                        OperatorID = Operators.options[n].value;
                        OperatorName = Operators.options[n].text;
                        MasterList.options[MasterList.length] = new Option(OperatorName, OperatorID);
                    }
                }
                for (var n = Operators.length - 1; n >= 0; n--)
                    if (Operators.options[n].selected)
                        Operators.remove(n);
            }
        }

        function reportComplete() {
            parent.window.setStatus('WebReport', false, 'DuplicateWells', false);

            previewNote();

            document.getElementById('ReportNote').style.display = 'none';
            frm.txtNote.value = document.getElementById('WebReportScroller').innerHTML;
            frm.action = 'WebUpdateSaveReport.asp';
            frm.submit();
        }

        function previewNote() {
            if (frm.txtNote.value == '')
                document.getElementById('Note').innerHTML = '';
            else
                document.getElementById('Note').innerHTML = '<b>Note</b><br>' + frm.txtNote.value;
        }

        function addAll(listFrom, listTo) {
            var sValue;
            var sText;

            if (listFrom.length != 0) {
                for (var n = 0; n < listFrom.length; n++) {
                    sValue = listFrom.options[n].value;
                    sText = listFrom.options[n].text;
                    listTo.options[listTo.length] = new Option(sText, sValue);
                }
                while (listFrom.length != 0)
                    listFrom.remove(0);
            }
        }

        function addSelected(listFrom, listTo) {
            var MasterList = document.frm.MasterList;
            var Groups = document.frm.Groups;
            var sValue;
            var sText;

            if (listFrom.length != 0) {
                for (var n = 0; n < listFrom.length; n++) {
                    if (listFrom.options[n].selected) {
                        sValue = listFrom.options[n].value;
                        sText = listFrom.options[n].text;
                        listTo.options[listTo.length] = new Option(sText, sValue);
                    }
                }
                for (var n = listFrom.length - 1; n >= 0; n--)
                    if (listFrom.options[n].selected)
                        listFrom.remove(n);
            }
        }

        function addAllGroups() {
            var MasterList = document.frm.MasterList;
            var Groups = document.frm.Groups;
            var GroupID;
            var GroupName;

            if (MasterList.length != 0) {
                for (var n = 0; n < MasterList.length; n++) {
                    GroupID = MasterList.options[n].value;
                    GroupName = MasterList.options[n].text;
                    Groups.options[Groups.length] = new Option(GroupName, GroupID);
                }
                while (MasterList.length != 0)
                    MasterList.remove(0);
            }
        }

        function addSelectedGroups() {
            var MasterList = document.frm.MasterList;
            var Groups = document.frm.Groups;
            var GroupID;
            var GroupName;

            if (MasterList.length != 0) {
                for (var n = 0; n < MasterList.length; n++) {
                    if (MasterList.options[n].selected) {
                        GroupID = MasterList.options[n].value;
                        GroupName = MasterList.options[n].text;
                        Groups.options[Groups.length] = new Option(GroupName, GroupID);
                    }
                }
                for (var n = MasterList.length - 1; n >= 0; n--)
                    if (MasterList.options[n].selected)
                        MasterList.remove(n);
            }
        }

        function removeAllGroups() {
            var MasterList = document.frm.MasterList;
            var Groups = document.frm.Groups;
            var GroupID;
            var GroupName;

            if (Groups.length != 0) {
                for (var n = 0; n < Groups.length; n++) {
                    GroupID = Groups.options[n].value;
                    GroupName = Groups.options[n].text;
                    MasterList.options[MasterList.length] = new Option(GroupName, GroupID);
                }
                while (Groups.length != 0)
                    Groups.remove(0);
            }
        }

        function removeSelectedGroups() {
            var MasterList = document.frm.MasterList;
            var Groups = document.frm.Groups;
            var GroupID;
            var GroupName;

            if (Groups.length != 0) {
                for (var n = 0; n < Groups.length; n++) {
                    if (Groups.options[n].selected) {
                        GroupID = Groups.options[n].value;
                        GroupName = Groups.options[n].text;
                        MasterList.options[MasterList.length] = new Option(GroupName, GroupID);
                    }
                }
                for (var n = Groups.length - 1; n >= 0; n--)
                    if (Groups.options[n].selected)
                        Groups.remove(n);
            }
        }

        function selectAllRegions() {
            for (var n = 0; n < frm.Region.length; n++)
                frm.Region.options[n].selected = true;
        }

        function validateAR() {
            if (frm.Operators.length == 0)
                alert('Please select one or more Operators from the list before continuing. ');
            else {
                if (frm.Operators.length < 41) {
                    for (var n = 0; n < frm.Operators.length; n++)
                        frm.Operators.options[n].selected = true;
                }
                else {
                    for (var n = 0; n < frm.Operators.length; n++)
                        frm.Operators.options[n].selected = false;
                }

                frm.Mode.value = 'ARCheck';
                frm.action = 'WebUpdateStart.asp';
                frm.submit();
            }
        }

        function validateGroups() {
            if (frm.Groups.length == 0)
                alert('Please select one or more Groups before continuing.');
            else {
                frm.Review.value = parent.window.document.frm.Review.value;
                frm.Year.value = 2000 + parseInt(parent.window.document.frm.Year.value, 10);
                for (var n = 0; n < frm.Groups.length; n++)
                    frm.Groups.options[n].selected = true;

                frm.action = 'DataExtractSummaryCharts3.asp';
                frm.submit();
            }
        }

        function skipCharts() {
            parent.window.setStatus('SummaryCharts', false, 'WebReport', false);

            frm.action = 'WebUpdateStart.asp';
            frm.Mode.value = 'WebReport';
            frm.submit();
        }

        function validateRegions() {
            if (frm.Region.length == 0)
                alert('Please select one or more Regions before continuing.');
            else {
                for (var n = 0; n < frm.Region.length; n++)
                    frm.Region.options[n].selected = true;

                frm.Review.value = parent.window.document.frm.Review.value;
                frm.Year.value = parent.window.document.frm.Year.value;
                frm.action = 'ImportRegionalSpreadsheet2.asp';
                frm.submit();
            }
        }

        function newOperatorLogin() {
            document.getElementById('OperatorName').style.display = 'none';
            document.getElementById('cboOperator').style.display = 'block';
            document.getElementById('OperatorLoginTitle').innerText = 'Create a New Operator Login';
            frm.bNewOperatorLogin.value = 'yes';
            document.getElementById('OperatorLogin').style.display = 'Block';
            frm.txtUsername.value = '';
            frm.txtPassword.value = '';
            frm.chkGiveAccess.checked = true;

        }

        function editOperatorLogin(OperatorID, OperatorName, Username, Password, Access) {
            document.getElementById('cboOperator').style.display = 'none';
            document.getElementById('OperatorName').innerText = OperatorName;
            document.getElementById('OperatorName').style.display = 'block';
            frm.bNewOperatorLogin.value = '';
            frm.OperatorID.value = OperatorID;
            frm.txtUsername.value = Username;
            frm.txtPassword.value = Password;

            frm.chkGiveAccess.checked = Access;
            frm.chkHadAccess.value = Access ? 'yes' : '';

            document.getElementById('OperatorLogin').style.display = 'Block';
        }

        function validateOperatorLogin() {
            var sMsg = '';

            if (frm.bNewOperatorLogin.value != '') {
                if (frm.cboOperator.options[frm.cboOperator.selectedIndex].value == '0') {
                    sMsg = 'Please select an Operator from the list before saving.';
                    frm.cboOperator.focus();
                }
            }

            if (sMsg == '') {
                if (frm.txtUsername.value == '') {
                    sMsg = 'Please enter a Username for this Operator';
                    frm.txtUsername.select();
                    frm.txtUsername.focus();
                }
                else if (frm.txtPassword.value == '') {
                    sMsg = 'Please enter a Password for this Operator';
                    frm.txtPassword.select();
                    frm.txtPassword.focus();
                }
            }

            if (sMsg != '')
                alert(sMsg);
            else {
                frm.Mode.value = 'WebUsers';
                frm.DoWhat.value = 'SAVE';
                frm.action = 'WebUpdateStart.asp';
                frm.submit();
            }
        }

        function validateReviewYear() {
            var sMsg = '';
            var sReview = frm.cboReview.options[frm.cboReview.selectedIndex].value;
            var sYear = frm.cboYear.options[frm.cboYear.selectedIndex].value;

            if (sReview == '') {
                sMsg = 'Please choose a Review before continuing';
                frm.cboReview.focus();
            }
            else if (sYear == '') {
                sMsg = 'Please choose a Year before continuing';
                frm.cboYear.focus();
            }

            if (sMsg != '')
                alert(sMsg);
            else {
                //Update display fields on main window
                parent.window.document.all.dispReview.innerText = sReview;
                parent.window.document.all.dispYear.innerText = '20' + sYear;
                parent.window.document.all.dispTitle.innerText = 'Web Update for :';

                //Update hidden fields on main window
                parent.window.document.frm.Review.value = sReview;
                parent.window.document.frm.Year.value = sYear;

                //Set hidden fields on this page
                frm.Review.value = sReview;
                frm.Year.value = 2000 + parseInt(sYear, 10); //need the 10 to tell parseInt to convert using base 10, 0-7 ok, but 8 and 9 = 0 by default

                //show tick
                parent.window.setStatus('ReviewYear', false, 'AR', false);
                //parent.window.setStatus('ReviewYear',false,'Macros',true);			

                //document.getElementById('tblReviewYear').style.display='none';
                //Need to remove the next line
                frm.Mode.value = 'AR';
                frm.Action = "WebUpdateStart.asp"
                frm.submit();
            }
        }

        function afterARCalculated() {
            parent.window.setStatus('AR', false, 'Macros', true);
            document.getElementById('ARTable').style.display = 'none';
        }
        function showRestoreResult() {
            if (aRecord[1] == 0) {
                parent.window.setStatus('Upload', false, 'Complete', false);
                document.getElementById('UploadStatus').innerHTML = 'Web Update Complete';
                parent.window.document.frm.UpdateComplete.value = 'yes';
            }
            else
                document.getElementById('restore').innerHTML = '<strong>ERROR: There was a problem restoring the database on the Dedicated Server - See Ray</strong>';
        }	
	
    </script>
    <style type="text/css">
        body
        {
            background-color: #F1DBD3;
        }
    </style>
</head>
<body>
    <form id="frm" runat="server" method="post" action="WebUpdateStart.aspx">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <% if (strMode == "ReviewYear")
       {%>
    <div id="preupdate" class="SectionTextBold" style="text-align: center;">
        Please wait.&nbsp;&nbsp;&nbsp;A backup is being taken before starting the Web Update&nbsp;
        <img src="Images/dotdotdot.gif" align="bottom" alt="" /></div>
    <table id="tblReviewYear" width="40%" border="0" align="center" cellpadding="5" cellspacing="1"
        class="DataRowSelected">
        <tr>
            <td width="37%" class="SectionTextBold">
                Select Review :
            </td>
            <td width="63%" class="DataRow1">
                <select name="cboReview" class="inputtext" id="cboReview">
                    <option value="" selected="selected">&lt; Select Review &gt;</option>
                    <option value="DPR">DPR</option>
                    <option value="CPR">CPR</option>
                </select>
            </td>
        </tr>
        <tr>
            <td class="SectionTextBold">
                Select Year :
            </td>
            <td class="DataRow1">
                <select name="cboYear" class="inputtext" id="cboYear">
                    <option value="">&lt; Select Year &gt;</option>
                    <option value="11">2011</option>
                    <option value="10">2010</option>
                    <option value="09" selected>2009</option>
                    <option value="08">2008</option>
                    <option value="07">2007</option>
                    <option value="06">2006</option>
                    <option value="05">2005</option>
                    <option value="04">2004</option>
                    <option value="03">2003</option>
                    <option value="02">2002</option>
                    <option value="01">2001</option>
                    <option value="00">2000</option>
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" class="DataHeader">
                <button id="btnReviewYear" class="inputtext" value="1" type="button" onclick="validateReviewYear()">
                    Continue</button>
            </td>
        </tr>
    </table>
    <%} %>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
      <% if (strMode == "AR")
       {%>
    <table width="80%" border="0" align="center" cellpadding="5" cellspacing="1" class="DataHeaderDark">
        <tr>
            <td colspan="3" valign="bottom" class="SectionTextBold">
                Select which Operators to calculate Access Restrictions for ...
            </td>
        </tr>
        <tr class="DataRow1">
            <td width="44%" align="center">
                <span class="SectionTextBold">
                    <br />
                    <select name="MasterList" size="20" multiple="multiple" class="inputtext" id="MasterList">
                    </select>
                    <br />
                    <br />
            </td>
            <td width="12%" align="center">
                <br />
                <input type="button" name="Button" title="Add ALL Operators to calculation" value="&gt;&gt;"
                    onclick="addAll(frm.MasterList,frm.Operators)" />
                <br />
                <input type="button" name="Submit3" title="Add Highlighted Operators to calculation"
                    value=" &gt; " onclick="addSelected(frm.MasterList,frm.Operators)" />
                <br />
                <br />
                <br />
                <input type="button" name="Submit4" title="Remove Highlighted Operators from calculation"
                    value=" &lt; " onclick="addSelected(frm.Operators,frm.MasterList)" />
                <br />
                <input type="button" name="Submit5" title="Remove all Operators from calculation"
                    value="&lt;&lt;" onclick="addAll(frm.Operators,frm.MasterList)" />
                <br />
            </td>
            <td width="44%" align="center">
                <span class="SectionTextBold">
                    <br />
                    Selected Operators </span>
                <select name="Operators" size="20" multiple="multiple" class="inputtext" id="Operators">
                </select>
                <br />
                <br />
            </td>
        </tr>
        <tr align="right">
            <td colspan="3" class="DataHeader">
                <br />
                <button value="1" type="button" class="inputtext" id="btnOperators" onclick="validateAR()">
                    Calculate</button>
            </td>
        </tr>
    </table>
      <%} %>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
     <% if (strMode == "ARCheck")
       {%>
    <table width="75%" border="0" align="center" cellpadding="5" cellspacing="1" class="DataRowSelected"
        id="ARTable">
        <tr>
            <td class="DataHeaderDark">
                Calculating Access Restrictions
            </td>
        </tr>
        <tr>
            <td align="center" class="DataRow1">
                <br />
                <br />
                <div id="ARText" style="text-align: center;">
                    Please wait whilst the Access Restrictions are being calculated
                    <img src="Images/dotdotdot.gif" width="30" height="3" align="bottom" alt=""></div>
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="right" class="DataHeader">
                <br />
                <div id="ARButton" style="display: none;">
                    <button value="1" type="button" class="inputtext" id="btnContinueAR" onclick="afterARCalculated()">
                        Continue</button></div>
            </td>
        </tr>
    </table>
        <%} %>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    </form>
</body>
</html>
