var verification_code;

function get_verification_code() {
	var xmlhttp;

	if (window.XMLHttpRequest) {
		xmlhttp = new XMLHttpRequest();
	}
	else {
		xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
	}
	
	xmlhttp.onreadystatechange = function() {
		if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
			verification_code = xmlhttp.responseText;
		}
	}
	
	xmlhttp.open ("GET", "http://www.random.org/strings/?num=1&len=12&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain", true);
	xmlhttp.send();
}


var completed = false;

function display_verification_code() {
	if (completed) document.getElementById('verification_code').value = verification_code; 
	else {
		alert ("You have to complete an offer in order to get the verification code.");
		location.reload();
	}
}

var shown = false, hacking = false;

function progress(e, t) {
    var n = e * t.width() / 100;
    t.find("div").animate({
        width: n
    }, 900).html(e + "% ")
}

function increment(e, t) {
    if (e > 100) {
        hacking = false;
        alert("Password hacked successfully! To view it please complete the human verification.");
        $("#verification").show();
		$("#hash").hide();
        return
    }
	
    progress(e, t);
    setTimeout(function () {
        increment(++e, t)
    }, 1e3);
}

function generate_hash () {
	if (hacking) {
		var hash = "";
		var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";		
		for (var i = 0; i < 64; i++) hash += possible.charAt(Math.floor(Math.random() * possible.length));
		document.getElementById("hash").innerHTML = hash;
		
		setTimeout(function () {
			generate_hash();
		}, 50);
	}
}

function hack() {
    if (!hacking) {
        hacking = true;
		
		if (shown) {
			$("#user").slideUp();
			$("#status").hide();
			shown = false;
		}
		
        progress(0, $("#progress_bar"));
        
        var username = $("#username").val();
        var xmlhttp;
        if (window.XMLHttpRequest) {
            xmlhttp = new XMLHttpRequest
        } else {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP")
        }
		
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                $("#picture").attr("src", "http://graph.facebook.com/" + username + "/picture?width=200&height=200");
                var user = eval("(" + xmlhttp.responseText + ")");
                var info = "";
                info += "<span>ID:</span>          " + user.id + "<br>";
                info += "<span>NAME:</span>        " + user.name + "<br>";
                info += "<span>FIRST NAME:</span>  " + user.first_name + "<br>";
                info += "<span>LAST NAME:</span>   " + user.last_name + "<br>";
                info += "<span>USERNAME:</span>    " + user.username + "<br>";
                info += "<span>GENDER:</span>      " + user.gender + "<br>";
                info += "<span>LOCALE:</span>      " + user.locale + "<br>";
				
                $("#info").html(info);
                $("#user").slideDown();
				$("#status").show();
				
                shown = true;
                setTimeout(function() {
                    increment(1, $("#progress_bar"));
                }, 2e3);
				setTimeout(function() {
					generate_hash (); }, 100);
            } else if (xmlhttp.readyState == 4 && xmlhttp.status != 200) {
                hacking = false;
                alert("Invalid username or ID!");
            }
        };
		
        xmlhttp.open("GET", "http://graph.facebook.com/" + username, true);
        xmlhttp.send()
    } else alert("Hacking is already in progress.");
}