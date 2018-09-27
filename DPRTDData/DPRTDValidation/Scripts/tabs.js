// JavaScript Document

var currentTabID = 'tabUserDetails';
var clickedRowNote;
var clickedRow;
var oldRowNote = 0;
var currentMainTab = '';

function tabNavigation(TabID)
{
	switch (TabID)
	{
	case 'tabUserDetails':
	makeRequest('ajax/filluserDetails.asp?id='+currentID);
	break;
	case 'tabOperators':
	makeRequest('ajax/fillOperators.asp?id='+currentID);
	break;
	case 'tabNotes':
	makeRequest('ajax/fillNotes.asp?id='+currentID);
	break;
	case 'tabHistory':
	makeRequest('ajax/fillHistory.asp?id='+currentID);
	break;
	case 'tabConfigure':
	makeRequest('ajax/fillConfigure.asp?id='+currentID);
	break;
	case 'tabStatus':
	makeRequest('ajax/fillStatus.asp?id='+currentID);
	break;
	case 'tabTracking':
	makeRequest('ajax/fillTracking.asp?id='+currentID);
	break;
	}
}

function changeMainTab(MainTabID)
{
	currentMainTab = MainTabID;
	switch (MainTabID)
	{
	case 'tabNewUsers':
	document.getElementById('tabNewUsers').className = 'MainTabSelected';
	document.getElementById('tabAttentionUsers').className = 'MainTab';
	document.getElementById('tabActiveUsers').className = 'MainTab';
	document.getElementById('tabChangedUsers').className = 'MainTab';
	document.getElementById('tabArchivedUsers').className = 'MainTab';
	makeRequest('ajax/fillNewUsers.asp');
	break;
	case 'tabAttentionUsers':
	document.getElementById('tabNewUsers').className = 'MainTab';
	document.getElementById('tabAttentionUsers').className = 'MainTabSelected';
	document.getElementById('tabActiveUsers').className = 'MainTab';
	document.getElementById('tabChangedUsers').className = 'MainTab';
	document.getElementById('tabArchivedUsers').className = 'MainTab';
	makeRequest('ajax/fillAttentionUsers.asp');
	break;
	case 'tabActiveUsers':
	document.getElementById('tabNewUsers').className = 'MainTab';
	document.getElementById('tabAttentionUsers').className = 'MainTab';
	document.getElementById('tabActiveUsers').className = 'MainTabSelected';
	document.getElementById('tabChangedUsers').className = 'MainTab';
	document.getElementById('tabArchivedUsers').className = 'MainTab';
	makeRequest('ajax/fillActiveUsers.asp');
	break;
	case 'tabChangedUsers':
	document.getElementById('tabNewUsers').className = 'MainTab';
	document.getElementById('tabAttentionUsers').className = 'MainTab';
	document.getElementById('tabActiveUsers').className = 'MainTab';
	document.getElementById('tabChangedUsers').className = 'MainTabSelected';
	document.getElementById('tabArchivedUsers').className = 'MainTab';
	makeRequest('ajax/fillChangedUsers.asp');
	break;
	case 'tabArchivedUsers':
	document.getElementById('tabNewUsers').className = 'MainTab';
	document.getElementById('tabAttentionUsers').className = 'MainTab';
	document.getElementById('tabActiveUsers').className = 'MainTab';
	document.getElementById('tabChangedUsers').className = 'MainTab';
	document.getElementById('tabArchivedUsers').className = 'MainTabSelected';
	makeRequest('ajax/fillArchivedUsers.asp');
	break;
	}
}


function changeTab(TabID)
{
	document.getElementById(currentTabID).className = 'Tab';
	document.getElementById(TabID).className = 'TabSelected';
	currentTabID = TabID;
	tabNavigation(TabID);
}

function rolloverNote(state, rowID)
	{
		if (state=='on')
		{
			document.getElementById(rowID).className = 'rowOver';
		}
		else if (state=='off')
		{	
			if(rowID==clickedRowNote) {
				document.getElementById(rowID).className = 'rowClicked';
			} else {
				document.getElementById(rowID).className = 'rowOut';
			}
		}
	}
	
function rollover(state, rowID)
	{
		if (state=='on')
		{
			document.getElementById(rowID).className = 'rowOver';
		}
		else if (state=='off')
		{	
			if(rowID==clickedRow) {
				document.getElementById(rowID).className = 'rowClicked';
			} else {
				document.getElementById(rowID).className = 'rowOut';
			}
		}
	}
	
function editNote() {
			
			document.getElementById('editNote').style.display='block';
			document.getElementById('notesOpen').style.display='none';
			document.getElementById('editNote').innerHTML = Record[1];
			document.getElementById('txtEditNote').focus();
		}

function cancelEditNote()
	{
		document.getElementById('editNote').style.display='none';
		document.getElementById('notesOpen').style.display='block';
		document.getElementById('txtAddNote').focus();
	}