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
		createCookie(name, "", -1);
}

function getBaseUrl() {
	return window.location.host.indexOf('localhost') > -1 ? "http://localhost:58095/" : "http://jehovajirehwebapi.cloudapp.net/";
}

var donationStatus = [
	{ Value: 0, Text: "Created" },
	{ Value: 1, Text: "PartialRequested" },
	{ Value: 2, Text: "Requested" },
	{ Value: 3, Text: "Canceled" },
	{ Value: 4, Text: "Scheduled" },
	{ Value: 5, Text: "Delivery" },
	{ Value: 6, Text: "Matched" }
];

var donationType = [
	{Value: 0, Text: "Vehicles"},
	{ Value: 1, Text: "Clothing" },
	{ Value: 2, Text: "Books" },
	{ Value: 3, Text: "Furniture" },
	{ Value: 4, Text: "ShoesAndAccessories" },
	{ Value: 5, Text: "HouseholdItems" },
	{ Value: 6, Text: "Linens" },
	{ Value: 7, Text: "SmallElectricalItems" },
	{ Value: 8, Text: "PianosAndOrgans" },
	{ Value: 9, Text: "ElectronicItems" },
	{ Value: 10, Text: "HouseholdAppliances" },
	{ Value: 11, Text: "Mattresses" },
	{ Value: 12, Text: "Tools" },
	{ Value: 13, Text: "Garden" },
	{ Value: 14, Text: "SchoolSupplies" },
	{ Value: 15, Text: "Money" },
	{ Value: 16, Text: "Other" }
]

function displayFieldTextByArray(id, array) {
	var result = null;

	if (!(id === undefined || id === "" || id === null || id === "00000000-0000-0000-0000-000000000000") && !(array == undefined || array == "" || array == null)) {
		result = $.grep(array, function (n, i) {
			return n.Value === id;
		});
	}

	return result != null ? (result.length > 0?  result[0].Text !== undefined ? result[0].Text : "": "") : "";
}

