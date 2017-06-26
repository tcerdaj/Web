ko.validation.init({
	registerExtenders: true,
	messagesOnModified: true,
	insertMessages: true,
	parseInputAttributes: true,
	messageTemplate: null
}, true);

data = function () { };

var RegisterViewModel = function (data) {
	var self = this;

	data = data || {};

	//If the data is coming from edit, use it to fill the fields.
	if (data !== null) {
		self.fileData = ko.observable({
			dataURL: ko.observable(data.FileData)
			// base64String: ko.observable(),
		});

		self.fileData().dataURL.subscribe(function (dataURL) {
			// dataURL has changed do something with it!
		});

		self.onClear = function (fileData) {
			if (confirm('Are you sure?')) {
				fileData.clear && fileData.clear();
			}
		};
		self.UserName = ko.observable(data.UserName || '').extend({
			required: true,
			maxLength: 35
		});

		//Properties from NameEntity
		self.FirstName = ko.observable(data.FirstName || '').extend({
			required: true,
			maxLength: 35
		});

		self.LastName = ko.observable(data.LastName || '').extend({
			required: true,
			maxLength: 35
		});

		self.Gender = ko.observable(data.Gender || '');
		self.Email = ko.observable(data.Email || '').extend({
			pattern: {
				params: /^([\d\w-\.]+@([\d\w-]+\.)+[\w]{2,4})?$/,
				message: "Invalid email address."
			} 
		});
		self.PhoneNumber = ko.observable(data.PhoneNumber || '');
		self.IsChurchMember = ko.observable(data.IsChurchMember || '');
		self.ChurchName = ko.observable(data.ChurchName || '');
		self.ChurchAddress = ko.observable(data.ChurchAddress || '');
		self.ChurchPhone = ko.observable(data.ChurchPhone || '');
		self.ChurchPastor = ko.observable(data.ChurchPastor || '');
		self.NeedToBeVisited = ko.observable(data.NeedToBeVisited || '');
		self.Comments = ko.observable(data.Comments || '');
		self.Address = ko.observable(data.Address || '').extend({
			maxLength: 80
		});
		self.City = ko.observable(data.City || '').extend({
				maxLength: 31
			});
		self.State = ko.observable(data.State || '').extend({
			maxLength: 21
		});
		self.Zip = ko.observable(data.Zip || '').extend({
			maxLength: 13
		});
	}

	//Computed Name property
	self.FullName = ko.computed(function () { return self.FirstName() + ' ' + self.LastName(); });

	// Sex List  
	self.Sex = [{ SexValue: "", SexLabel: "" },
	{ SexValue: 0, SexLabel: "Male" },
	{ SexValue: 1, SexLabel: "Female" }];

	self.isSubmiting = ko.observable(false);
	self.showChurchMember = ko.observable(function () {
		return this.IsChurchMember();
	});

	self.errors = ko.validation.group(this),

		self.submit = function (formElement) {

			if (!self.isSubmiting()) {
				//If no errors please submit the form
				if (self.errors().length === 0) {
					self.isSubmiting(true);
					return true;
				}
				else {
					self.isSubmiting(false);
					//if Errors display the messages
					self.errors.showAllMessages();
					$('.alert-danger.formError').remove();
					var new_span = $("<span class='alert-danger' style=margin-left:10px> Please check your information!</span>").addClass('formError');
					new_span.insertAfter("#create");
					//setTimeout(function ()
					//{
					//   $('.alert-danger.formError').remove();
					//}, 2000);
					return false;
				}
			}
		};
};


