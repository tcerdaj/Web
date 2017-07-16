ko.validation.init({
	registerExtenders: true,
	messagesOnModified: true,
	insertMessages: true,
	parseInputAttributes: true,
	messageTemplate: null
}, true);

data = function () { };
var itemTypes = [];
var DonationLine = function () {
	var self = this;

	self.Index = ko.observable(0);
	self.ItemType = ko.observable().extend({ required: { message: 'Donation Type is required' } });;
	self.ItemName = ko.observable().extend({ required: { message: 'Item Name is required' } });
	self.ImageUrl = ko.observable();
	self.DonationStatus = ko.observable();
	self.WantThis = ko.observable(false);
	
	self.fileData = ko.observable({
		dataURL: ko.observable(),
		// base64String: ko.observable(),
	});

	self.MultiFileData = ko.observable({
		dataURLArray: ko.observableArray(),
		base64String: ko.observable()
	});


	self.ItemTypes = ko.observableArray(itemTypes);
	self.onClear = function (imageFile) {
		if (confirm('Are you sure?')) {
			imageFile.clear && imageFile.clear();
		}
	};

	self.errors = ko.validation.group(self);
}

var MakeDonationViewModel = function (data) {
	var self = this;
	data = data || {};

	if (data !== null) {

		//========** Properties **=============

		self.Title = ko.observable(data.Title || '').extend({
			required: true,
			maxLength: 50
		});

		self.Description = ko.observable(data.Description || '').extend({
			required: true,
			maxLength: 180
		});

		itemTypes = data.ItemTypes;
		self.Amount = ko.observable(data.Amount || 0.0).extend({ numeric: 2 });
		self.IsMoney = ko.observable(data.IsMoney || false);
		self.ExpireOn = ko.observable(moment().add(3, 'M').format('YYYY-MM-DD'));
		self.ItemTypes = ko.observableArray(data.ItemTypes);
		self.DonationDetails = ko.observableArray([new DonationLine(data.DonationDetails)]);
		self.errors = ko.validation.group(self);
		self.ShowAddButton = ko.observable(false);
		self.onIsMoneyChange = function () {
			if (!this.IsMoney())
				this.Amount('');
		};

		//========**Computed properties**=============



		//========** Functions **=============

		self.CanShowDetails = function () {
			return self.Title !== '' && self.Description !== '';
		};


		//========** Events **=============
		// Operations
		self.addLine = function () {

			if (this.errors().length === 0) {
				var list = self.DonationDetails();
				var index = list.length - 1;
				var line = list[index];
				line.Index(index);
				self.DonationDetails.push(new DonationLine())
				self.ShowAddButton(true);
			}
			else {
				alert('Please check your submission.');

				if (this.errors().length > 0)
					this.errors.showAllMessages(true);

				this.errors().forEach(function (data) {
					alert(data.error);
				});
			}
		};
		self.removeLine = function (line) { self.DonationDetails.remove(line) };
	}

	self.submit = function (e) {
		var form = new FormData($('.page-make-donation.form-horizontal')[0]);
		var details = [];
		var index = 0;

		this.DonationDetails().forEach(function (e) {
			var u = ko.mapping.toJS(e); 
			details.push({
				Index: index,
				ItemType: u.ItemType,
				ItemName: u.ItemName,
				ImageUrl: u.ImageUrl,
				DonationStatus: u.DonationStatus,
				WantThis: u.WantThis,
				MultiFileData: u.MultiFileData.fileArray
			});
			index++;
		});

		form.append("details", JSON.stringify(details));

		//Ajax call to Insert the Employee
		$.ajax({
			type: "POST",
			url: "/Donations/MakeADonation",
			data: form,
			enctype: 'multipart/form-data',
			processData: false,  // Important!
			contentType: false,
			cache: false,
			timeout: 600000,
			success: function (response) {
				if (response.result === 'Redirect')
					window.location = response.url;
			},
			error: function (ex) {
				alert(ex.statusText);
			}
		});

	};
	
};





