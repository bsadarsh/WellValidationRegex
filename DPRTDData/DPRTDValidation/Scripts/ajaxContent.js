// JavaScript Document

var waster;

function fillUserDetails()
{
	document.getElementById('detailsContent').innerHTML = Record[1];
	if (document.getElementById('title'))
	document.getElementById('title').focus();
}

function fillOperatorsDetails()
{
	document.getElementById('detailsContent').innerHTML = Record[1];
	document.getElementById('groups').focus();
}

function fillUserNotes()
{
	document.getElementById('detailsContent').innerHTML = Record[1];
	document.getElementById('txtAddNote').focus();
	document.getElementById('txtAddNote').select();
}

function fillUserHistory()
{
	document.getElementById('detailsContent').innerHTML = Record[1];
}

function fillUserConfigure()
{
	document.getElementById('detailsContent').innerHTML = Record[1];
}

function fillUserStatus()
{
	document.getElementById('detailsContent').innerHTML = Record[1];
}

function fillOperatorSelections()
{
	//alert(document.getElementById('OperatorSelections').innerHTML);
	//alert(Record[1]);
	document.getElementById('OperatorSelections').innerHTML = Record[1];	
}

function addOperatorSelection(from,to)
	{
		var cboFrom = document.getElementById(from);
		var cboTo = document.getElementById(to);
		
		if (cboFrom.selectedIndex!=-1)
		{
			cboTo.options[cboTo.length] = new Option(cboFrom.options[cboFrom.selectedIndex].text, cboFrom.value, false, false);
			cboFrom.options[cboFrom.selectedIndex] = null;
		}
	}
	
function editUserDetails()
{
	//alert('Updated!');
	var i=0;
}


function fillNewUsers()
{
	document.getElementById('UserContent').innerHTML = Record[1];
	document.getElementById('searchField').select();
	document.getElementById('searchField').focus();
}

function fillAttentionUsers()
{
	document.getElementById('UserContent').innerHTML = Record[1];
	document.getElementById('searchField').select();
	document.getElementById('searchField').focus();
}

function fillAttentionUsersList()
{
	document.getElementById('attentionUsersList').innerHTML = Record[1];
	document.getElementById('searchField').focus();
}

function fillNewUsersList()
{
	document.getElementById('newUsersList').innerHTML = Record[1];
	document.getElementById('searchField').focus();
}

function fillActiveUsers()
{
	document.getElementById('UserContent').innerHTML = Record[1];
	document.getElementById('searchField').select();
	document.getElementById('searchField').focus();
}

function fillActiveUsersList()
{
	document.getElementById('activeUsersList').innerHTML = Record[1];
	document.getElementById('searchField').focus();
}

function fillArchivedUsers()
{
	document.getElementById('UserContent').innerHTML = Record[1];
	document.getElementById('searchField').select();
	document.getElementById('searchField').focus();
}