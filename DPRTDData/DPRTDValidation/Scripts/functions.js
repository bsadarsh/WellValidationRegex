// Goto Destination functions

function trapError()
{
	return true;
}
function submitForm(destination)
{
	var bOK = false;
	
	self.onerror = trapError;
	
	if (destination=='cancel')
		bOK = true;
	else if (validate())
		bOK = true;
		
	if (bOK)
	{
		document.frm.Destination.value = destination;
		document.frm.submit();
	}
	
	self.onerror = null;
}
function popupWindow(destination, winName, nLeft,nTop,nWidth,nHeight)
{
	var newWindow;
	var sSize = "";
	
	if (popupWindow.arguments.length!=2)
		sSize ="left="+nLeft+",top="+nTop+",width="+nWidth+",height="+nHeight+",";

	newWindow = window.open(destination,winName,sSize+'toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbar=no,resizable=no');
}
function popupWindowResize(destination, winName, nLeft,nTop,nWidth,nHeight)
{
	var newWindow;
	var sSize = "";
	
	if (popupWindowResize.arguments.length!=2)
		sSize ="left="+nLeft+",top="+nTop+",width="+nWidth+",height="+nHeight+",";

	newWindow = window.open(destination,winName,sSize+'toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbar=yes,resizable=yes');
}
function fullScreen()
{
	window.moveTo (0, 0);
	if (document.layers) // nav4+
	{
		var X = screen.availWidth - window.outerWidth;
		var Y = screen.availHeight - window.outerHeight;
		window.resizeBy (X, Y);
	}
	else if (document.all) // exp4+
	{
		resizeTo(screen.availWidth,screen.availHeight);
	}
}
function setStatus(someText)
{
	window.status = someText;
}
function wizardPopup(url) 
{
    var options = "left=150,top=100,width=700,height=400,";
    options += "resizable=no,scrollbars=no,status=no,";
    options += "menubar=no,toolbar=no,location=no,directories=no";
	
	var windowName = "wizard";
		
	var winPopup = window.open(url, windowName, options);
	winPopup.focus();
  }
  
function resultGroupClicked(obj,title)
{
	if(obj.className=='collapsed')
	{
		obj.className='expanded'; 
		document.getElementById(title+'submenu').style.display='block';
		document.getElementById(title+'expanded').value='yes';
	}
	else
	{
		obj.className='collapsed'; 
		document.getElementById(title+'submenu').style.display='none';
		document.getElementById(title+'expanded').value='';
	}
}  

function trim(a)
{
	return a.replace(/^\s+/,'').replace(/\s+$/,'')
}

