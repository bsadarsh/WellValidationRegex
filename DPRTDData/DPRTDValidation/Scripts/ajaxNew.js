	var Record;
	
	function makeRequest(url) {

        var http_request = false;

		//alert('in Request');
        if (window.XMLHttpRequest) { // Mozilla, Safari,...
            http_request = new XMLHttpRequest();
            if (http_request.overrideMimeType) {
                http_request.overrideMimeType('text/xml');
                // See note below about this line
            }
        } else if (window.ActiveXObject) { // IE
            try {
                http_request = new ActiveXObject("Msxml2.XMLHTTP");
            } catch (e) {
                try {
                    http_request = new ActiveXObject("Microsoft.XMLHTTP");
                } catch (e) {}
            }
        }

        if (!http_request) {
            alert('Giving up :( Cannot create an XMLHTTP instance');
            return false;
        }
        http_request.onreadystatechange = function() { alertContents(http_request); };
        http_request.open('GET', url, true);
        http_request.send(null);

    } // makeRequest ends here!

    function alertContents(http_request) {

		//alert('in Alert');
        if (http_request.readyState == 4) {
            if (http_request.status == 200) {
                updateContents(http_request.responseText);
            } else if (http_request.status == 404) {
                alert('File was not found.');
            } else {
				alert('Error Occurred:' + http_request.status );
			}
        }

    }
	
	function updateContents(data)
	{
		//alert('in Update');
		//alert(data);
		//document.getElementById('help').value = data;
		Record = data.split('[row]');
		setTimeout(Record[0],1);
	}