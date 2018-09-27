	var helpActive = false;

	var speechHelpOn = '<span class="SectionTextBold">Help</span><br>Click here to turn on the context sensitive help feature.<br><br>This means that when you hover over a link, a box like this will be displayed providing more information about what will happen when you click on the link.';
	var speechHelpOff = '<span class="SectionTextBold">Help</span><br>Click here to turn off the context sensitive help feature for this page.';	
	var speechLogout = '<span class="SectionTextBold">Logout</span><br>Click on this link to log out of the RushmoreReviews.com website.<br><br>You will be returned to the login page.';
	
	function getPageXY(objID)
	{
		var XY = {x:0, y:0, w:0, h:0};
		var el = document.getElementById(objID);
		
		XY.w = el.offsetWidth;
		XY.h = el.offsetHeight;
		
		for( var node = el; node; node=node.offsetParent)
		{ 
			XY.x += node.offsetLeft;
			XY.y += node.offsetTop;
		} 
		return XY;
	}
	
	function setHelpText(theSpeechText)
	{
		document.all.speechText.innerHTML = theSpeechText; 
	}

	function speechTop(objID)
	{
		var xy = getPageXY(objID);
		return xy.y+document.getElementById(objID).offsetHeight;
	}
		
	function speechLeft(objID)
	{
		var xy = getPageXY(objID);
		var speech = getPageXY('speechLayer');
		return xy.x-speech.w+5; //173;
	}
	
	function speechRight(objID)
	{
		var xy = getPageXY(objID);
		var speech = getPageXY('speechLayer');
		return xy.x+xy.w-5; //document.getElementById(objID).offsetWidth-26;
	}
	
	function mouseOverHelp(objID, speechText, width, onRight)
	{
		if (helpActive)
			showHelp(objID, speechText, width, onRight);
		else
		{
			helpActive = true;
			showHelp(objID, speechText, width, onRight);
			helpActive = false;
		}
	}
	
	function mouseOutHelp()
	{
		if (helpActive)
			hideHelp();
		else
		{
			helpActive = true;
			hideHelp();
			helpActive = false;
		}
	}
	
	function mouseClickHelp()
	{
		if (helpActive)
		{
			helpActive = false;
			document.all.HideShow.innerHTML = "Show Help";
		}
		else
		{
			helpActive = true;
			document.all.HideShow.innerHTML = "Hide Help";
		}
		mouseOutHelp();
	}

	function showHelp( objID, theSpeechText, w, pointRight)
	{
		if (helpActive) 
		{ 
			changeWidthTo('speechLayer', w);
			setHelpText(theSpeechText);
					
			var xPos = 0;
			var yPos = speechTop(objID);
			
			if (pointRight)
			{
				document.all.speechTD.align = "right";
				document.all.speechArrow.src = "images/speech1b.gif";
				xPos = speechLeft(objID);
			}
			else
			{
				document.all.speechTD.align = "left";
				document.all.speechArrow.src = "images/speech1a.gif";
				xPos = speechRight(objID);
			}
			shiftTo('speechLayer',xPos, yPos);
			show('speechLayer',0,23,0,document.getElementById('speechTable').clientHeight-document.getElementById('speechLayer').clientHeight-23) 
		}
	}
	
	function hideHelp()
	{
		if (helpActive) 
			hide('speechLayer');
	}
	