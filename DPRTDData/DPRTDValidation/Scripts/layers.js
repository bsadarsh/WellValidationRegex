// JavaScript Document
var nMenuLeft = 0;
var nMenuTop = 0;
var nMenuWidth = 0;
var nMenuHeight = 0;

var coll = "all.";
var styleObj = ".style";

function getObject(obj) {
	var theObj;
	
	if (typeof obj == "string") 
	{
		theObj = eval("document." + coll + obj + styleObj);
	} 
	else 
	{ 
		theObj = obj;
	} 
	
	return theObj;
}

function getDim(el)
{
	for (var lx=0,ly=0; el!=null; lx+=el.offsetLeft, ly+=el.offsetTop, el=el.offsetParent);
	return {x:lx,y:ly}
}

function checkPosition()
{
	//var theObj = getObject("anchorTable");
	var objLoc = getDim(document.getElementById("anchorTable"));

	shiftTo("CostsLayer", objLoc.x-134, objLoc.y+23);
}
function checkCalendarPosition()
{
	//var theObj = getObject("anchorTable");
	var objLoc = getDim(document.getElementById("ActionLayer"));

	shiftTo("Calendar", objLoc.x+63, objLoc.y-85);
}
function shiftTo(obj, x, y) 
{
	var theObj = getObject(obj);

	theObj.pixelLeft = x;
	theObj.pixelTop = y;
}

function getPositionAndSize(obj)
{
	var theObj = getObject(obj);

	nMenuLeft = theObj.posLeft;
	nMenuTop = theObj.posTop;
	nMenuWidth = parseInt(theObj.width);
	nMenuHeight = parseInt(theObj.height);
}

function changeSizeTo(obj, x, y) 
{
	var theObj = getObject(obj);
	
	theObj.width = x+"px";
	theObj.height = y+"px"; //deltaY;
}

function changeWidthTo(obj, w)
{
	var theObj = getObject(obj);

	theObj.width = w+"px";
}

function hideShowLayer(whichLayer, hideShow)
{
	var theObj = getObject(whichLayer);
	theObj.visibility = hideShow;
}


function show(whichLayer, leftOffset, topOffset, widthOffset, heightOffset)
{
	//alert(document.getElementById('speechTable').clientHeight);

	var theObj = getObject(whichLayer);
	theObj.visibility = "visible";
	
	if (show.arguments.length==1)
	{
		leftOffset = 0;
		topOffset = 0;
		widthOffset = 0;
		heightOffset = 0;
	}
	
	getPositionAndSize(whichLayer);
	shiftTo("layerFrame", nMenuLeft+leftOffset, nMenuTop+topOffset);
	changeSizeTo("layerFrame", nMenuWidth+widthOffset, nMenuHeight+parseInt(heightOffset));
	hideShowLayer("layerFrame", "visible");
}
function hide(whichLayer)
{
	hideShowLayer(whichLayer, "hidden");
	hideShowLayer("layerFrame", "hidden");
}
function moveRelativeTo(moveWhat, anchorObjectID, offsetX, offsetY)
{
	//var theObj = getObject("anchorTable");
	var objLoc = getDim(document.getElementById(anchorObjectID));

	shiftTo(moveWhat, objLoc.x+offsetX, objLoc.y+offsetY);
}


function showCalendar(theField, theDate)
{
	Calendar.location = "calendar.asp?caldate="+theDate+'&outputfield='+theField;
//alert(Calendar.location);
	show("Calendar",-34,0);
	
	//hideShowLayer("Calendar", "visible");
}
function hideCalendar()
{
	hideShowLayer("Calendar", "hidden");
}
function setCalendarValue(theField, theDate)
{
	eval(theField+".value='"+theDate+"'");
}