<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUpdate.aspx.cs" Inherits="DPRTDValidation.WebUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Update</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function testComplete() {
            setStatus('TestPreLive', true, 'Upload', false);
            document.getElementById('UpdateActions').src = 'WebUpdateStart.aspx?Mode=Upload&Review=' + frm.Review.value + '&Year=20' + frm.Year.value;
        }

        function setStatus(currentStep, bHideButton, nextStep, bHasButton) {
            //Show tick for the current step
            document.getElementById('tick' + currentStep).style.display = 'block';
            document.getElementById('div' + currentStep).className = 'divComplete';
            if (bHideButton)
                document.getElementById('btn' + currentStep).style.display = 'none';

            //Enable the next step and button if there is one
            document.getElementById('div' + nextStep).className = 'divEnabled';
            if (bHasButton)
                document.getElementById('btn' + nextStep).disabled = false;
        }
    </script>
    <style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	font-family: Arial, Helvetica, sans-serif;
	font-size: 100%;
	font-weight: normal;
	color: #000000;
	text-decoration: none;
}
div {
	margin-bottom: 5px;
	border-bottom-width: 1px;
	border-bottom-style: dotted;
	border-bottom-color: #E8C5B7;
	padding-bottom: 5px;
	display: block;
	text-align: left;
}
.divDisabled {
	font-size: 80%;
	font-weight: normal;
	color: #AAAAAA;
	text-decoration: none;
}
.divEnabled {
	font-size: 80%;
	font-weight: bold;
	color: #000000;
	text-decoration: none;
}
button {
	height: 20px;
	width: 45px;
	font-size: 80%;
	color: #000000;
}
.divComplete {
	font-size: 80%;
	font-weight: bold;
	color: #BE613D;
	text-decoration: none;
}
-->
</style>
</head>
<body>
    <table width="100%" border="0" cellspacing="10" cellpadding="5">
        <tr>
            <td width="30%" align="center" valign="top" class="Section">
                <span class="SectionTitle">Steps for performing a Web Update </span>
                <br />
                <br />
                <form name="frm" id="frm" method="post" action="">
                <input name="Review" type="hidden" value="" /><input name="Year" type="hidden" value="" />
                <div id="divReviewYear" class="divEnabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickReviewYear" style="display: none;" src="Images/tick.gif" width="16"
                                    height="16" alt="" />
                            </td>
                            <td width="60%">
                                <span id="dispTitle">Choose Review &amp; Year</span>
                            </td>
                            <td width="31%" align="right">
                                <span id="dispReview"></span>&nbsp;<span id="dispYear"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divAR" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickAR" style="display: none;" src="Images/tick.gif" alt="" width="16" height="16" />
                            </td>
                            <td width="76%">
                                Calculate Access Restrictions
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divMacros" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickMacros" style="display: none;" src="Images/tick.gif" alt="" width="16"
                                    height="16" />
                            </td>
                            <td width="76%">
                                Have the Macros been run ?
                            </td>
                            <td width="15%" align="right">
                                <input name="MacrosRun" type="hidden" value="" />
                                <button id="btnMacros" value="1" type="button" disabled="disabled" onclick="frm.MacrosRun.value='yes'; setStatus('Macros',true,'Wells',false); document.getElementById('UpdateActions').src = 'WebUpdateStart.aspx?mode=Regions'">
                                    Yes</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divWells" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickWells" style="display: none;" src="Images/tick.gif" width="16" height="16"
                                    alt="" />
                            </td>
                            <td width="76%">
                                Import Well Data
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSummaryCharts" class="divDisabled" style="display: none;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickSummaryCharts" style="display: none;" src="Images/tick.gif" width="16"
                                    height="16" alt="" />
                            </td>
                            <td width="76%">
                                Extract Data for Quick Charts
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divWebReport" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickWebReport" style="display: none;" src="Images/tick.gif" width="16" height="16"
                                    alt="" />
                            </td>
                            <td width="76%">
                                Create Web Update Report
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divDuplicateWells" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickDuplicateWells" style="display: none;" src="Images/tick.gif" width="16"
                                    height="16" alt="" />
                            </td>
                            <td width="76%">
                                View Duplicate Wells Report
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divWebUsers" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickWebUsers" style="display: none;" src="Images/tick.gif" width="16" height="16"
                                    alt="" />
                            </td>
                            <td width="76%">
                                Update Web Users
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divFTPCharts" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickFTPCharts" style="display: none;" src="Images/tick.gif" width="16" height="16"
                                    alt="" />
                            </td>
                            <td width="76%">
                                Have Charts been FTP'd to Fasthosts and Pre-Live ?
                            </td>
                            <td width="15%" align="right">
                                <button id="btnFTPCharts" value="1" type="button" onclick="setStatus('FTPCharts',true,'NoChange',false); document.getElementById('UpdateActions').src = 'WebUpdateStart.aspx?mode=NoChange'"
                                    disabled="disabled">
                                    Yes</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divNoChange" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickNoChange" style="display: none;" src="Images/tick.gif" width="16" height="16"
                                    alt="" />
                            </td>
                            <td width="76%">
                                Ask users not to make any changes on the Intranet until you tell them otherwise.
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divDTSPreLive" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickDTSPreLive" style="display: none;" src="Images/tick.gif" width="16"
                                    height="16" alt="" />
                            </td>
                            <td width="76%">
                                Transfer Data to the Pre-Live website
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divMakeChange" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickMakeChange" style="display: none;" src="Images/tick.gif" width="16"
                                    height="16" alt="" />
                            </td>
                            <td width="76%">
                                Inform users that they can use the Intranet again.
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divTestPreLive" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickTestPreLive" style="display: none;" src="Images/tick.gif" width="16"
                                    height="16" alt="" />
                            </td>
                            <td width="76%">
                                Test on the Pre-Live website
                            </td>
                            <td width="15%" align="right">
                                <button id="btnTestPreLive" value="1" type="button" disabled="disabled" onclick="testComplete()">
                                    Done</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divUpload" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                <img id="tickUpload" style="display: none;" src="Images/tick.gif" width="16" height="16"
                                    alt="" />
                            </td>
                            <td width="76%">
                                Upload Data to Dedicated Server
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divComplete" class="divDisabled">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="9%" height="25">
                                &nbsp;
                            </td>
                            <td width="76%">
                                &nbsp;
                            </td>
                            <td width="15%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <input name="UpdateComplete" type="hidden" value="" />
                <input name="btnClose" type="button" value="Close Window" onclick="if (frm.UpdateComplete.value=='') { if (confirm('If you are sure you want to abort this Web Update,\nclick the \'OK\' button below, otherwise click \'Cancel\'.  ')) { window.close(); } } else { window.close(); }" />
                </form>
            </td>
            <td valign="top" class="Section">
                <iframe id="UpdateActions" name="UpdateActions" frameborder="0" scrolling="auto"
                    style="height: 650px; width: 100%;" src="WebUpdateStart.aspx?mode=ReviewYear">
                </iframe>
            </td>
        </tr>
    </table>
</body>
</html>
