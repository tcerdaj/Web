/*This is a Common file that store all the function and utility that
whe need in the whole UI.*/


function createCookie(name, value, days) {
	var expires;

	if (days) {
		var date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		expires = "; expires=" + date.toGMTString();
	} else {
		expires = "";
	}
	document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
}

function readCookie(name) {
	var nameEQ = encodeURIComponent(name) + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) === ' ') c = c.substring(1, c.length);
		if (c.indexOf(nameEQ) === 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
	}
	return null;
}

function eraseCookie(name) {
	if (name !== '' && name !== undefined)
		createCookie(name, null, -1);
}

function getBaseUrl() {
    //return window.location.host.indexOf('localhost') > -1 ? "http://jehovajireh.web.service/" : "http://jehovajireh.web.service/";
   return window.location.host.indexOf('localhost') > -1 ? "http://localhost:58095/" : "http://jehovajireh.web.service/";
}

var donationStatus = function () {
	return $.ajax({
		url: "/Services/DonationStatus",
		datatype: "json",
		contentType: 'application/json',
		type: "GET",
		success: function (result) {
			console.log("success");
		},
		error: function (xhr, status, error) {
			alert(xhr.responseTest);
		}
	});
};

var donationTypes = function () {
	return $.ajax({
		url: "/Services/DonationTypes",
		datatype: "json",
		contentType: 'application/json',
		type: "GET",
		success: function (result) {
			console.log("success");
		},
		error: function (xhr, status, error) {
			alert(xhr.responseTest);
		}
	});
};

function displayFieldTextByArray(id, array) {
	var result = null;
	if (!(id === undefined || id === "" || id === null || id === "00000000-0000-0000-0000-000000000000") && !(array === undefined || array === "" || array === null)) {
		result = $.grep(array, function (n, i) {
			return n.Value === id.toString();
		});
	}
	return result !== null ? (result.length > 0?  result[0].Text !== undefined ? result[0].Text : "": "") : "";
}

function getItemByArray(uid, array) {
    var result = null;
    if (!(uid === undefined || uid === "" || uid === null || uid === "00000000-0000-0000-0000-000000000000") && !(array === undefined || array === "" || array === null)) {
        result = $.grep(array, function (n, i) {
            return n.uid === uid;
        });
    }
    return result !== null ? (result.length > 0 ? result[0]: null ) : null;
}

function findMemberFromList(id, array) {
    var result = null;
    if (!(id === undefined || id === "" || id === null || id === "00000000-0000-0000-0000-000000000000") && !(array === undefined || array === "" || array === null || array.length === 0)) {
        result = $.grep(array, function (n, i) {
            return n.ConnectionId === id.toString();
        });
    }
    return result !== null  && result.length > 0 ? result[0] : result;
}

function convertDictionaryToObject(dictionary) {
    var result = {};
    $.each(dictionary, function (index, value) {
        result[value["Key"]] = value["Value"];
    });

    return result;
}

var getInitials = function (string) {
    var names = string.split(' '),
        initials = names[0].substring(0, 1).toUpperCase();

    if (names.length > 1) {
        initials += names[names.length - 1].substring(0, 1).toUpperCase();
    }
    return initials;
};

// check if an element exists in array using a comparer function
// comparer : function(currentElement)
Array.prototype.inArray = function (comparer) {
    for (var i = 0; i < this.length; i++) {
        if (comparer(this[i])) return true;
    }
    return false;
}; 

// adds an element to the array if it does not already exist using a comparer 
// function
Array.prototype.pushIfNotExist = function (element, comparer) {
    if (!this.inArray(comparer)) {
        this.push(element);
    }
};

Array.prototype.removeIfExist = function (element, comparer) {
    if (this.inArray(comparer)) {
        this.slice(this.indexOf(element, 1));
    }
};

!function ($) {
    $.extend($.fn, {
        busyIndicator: function (c) {
            b = $(this);
            var d = b.find(".k-loading-mask");
            c ? d.length || (d = $("<div class='k-loading-mask'><span class='k-loading-text'>Loading...</span><div class='k-loading-image'/><div class='k-loading-color'/></div>").width(b.outerWidth()).height(b.outerHeight()).prependTo(b)) : d && d.remove()
        }
    });
}(jQuery);


function openDialog(title, htmlMessage) {

    var applySetup = $("#dialog").length === 0;
    var dialog = $("#dialog").length === 0 ? $('<div id="dialog"> </div >').appendTo($('.container')) : $("#dialog");

    if (applySetup) {
        dialog.kendoDialog({
            width: "400px",
            title: title,
            closable: true,
            modal: false,
            content: htmlMessage,
            actions: [
                { text: 'Ok', primary: true }
            ],
        });
    }
    else {
        dialog.data("kendoDialog").title(title);
        dialog.data("kendoDialog").content(htmlMessage);
    }

    dialog.data("kendoDialog").open();
}


