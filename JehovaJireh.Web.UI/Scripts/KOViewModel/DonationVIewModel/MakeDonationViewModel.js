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
	self.Index = ko.observable();
	self.ItemType = ko.observable().extend({ required: {message: 'Donation Type is required' } });;
	self.ItemName = ko.observable().extend({ required: { message: 'Item Name is required' } });
	self.ImageUrl = ko.observable();
	self.DonationStatus = ko.observable(0);
	self.WantThis = ko.observable(false);
	
	self.fileData = ko.observable({
		dataURL: ko.observable(),
		// base64String: ko.observable(),
	});
	self.MultiFileData = ko.observable({
		dataURLArray: ko.observableArray(),
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
		self.Amount = ko.observable(data.Amount || '');
		self.IsMoney = ko.observable(data.IsMoney || false);
		self.ExpireOn = ko.observable(data.ExpireOn || '');
		self.ItemTypes = ko.observableArray(data.ItemTypes);
		self.DonationDetails = ko.observableArray([new DonationLine()]);
		self.errors = ko.validation.group(self);
		//========**Computed properties**=============



		//========** Functions **=============

		self.CanShowDetails = function () {
			return self.Title !== '' && self.Description !== '';
		};


		//========** Events **=============
		// Operations
		self.addLine = function () {

			if (this.errors().length === 0) {
				self.DonationDetails.push(new DonationLine())
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


		self.submit = function () {
			//Ajax call to Insert the Employee
			$.ajax({
				type: "POST",
				url: "/Account/Register",
				data: ko.toJSON(PerData),
				contentType: "application/json",
				dataType: 'json',
				//cache: false,
				mimeType: "multipart/form-data",
				//processData: false,
				success: function () {
					alert("successful");
				},
				error: function () {
					alert("fail");
				}
			});
		};
};


