
/*@* ------------------------- date js---------------------* @*/


	$(document).ready(function () {
		let today = new persianDate();
		let maxDate = today.year() + '/12/29';

		// shamsi
		$('#shamsiDate').persianDatepicker({
			format: 'YYYY/MM/DD',
			calendarType: 'persian',
			initialValue: false,
			autoClose: true,
			maxDate: maxDate,
			onSelect: function (unix) {
				let pd = new persianDate(unix);
				let gd = pd.toDate();

				let gYear = gd.getFullYear();
				let gMonth = gd.getMonth() + 1;
				let gDay = gd.getDate();
				let gDate = `${gYear}-${String(gMonth).padStart(2, '0')}-${String(gDay).padStart(2, '0')}`;
				$('#gregorianDate').val(gDate);

				calculateAge(gYear, gMonth, gDay);

			}
		});

		// Gregorian
		$('#gregorianDate').persianDatepicker({
			format: 'YYYY-MM-DD',
			calendarType: 'gregorian',
			initialValue: false,
			autoClose: true,
			onSelect: function (unix) {
				let pd = new persianDate(unix).toCalendar('persian');
				let pYear = pd.year();
				let pMonth = pd.month();
				let pDay = pd.date();
				let pDate = `${pYear}/${String(pMonth).padStart(2, '0')}/${String(pDay).padStart(2, '0')}`;
				$('#shamsiDate').val(pDate);

				let gd = new Date(unix);
				let gYear = gd.getFullYear();
				let gMonth = gd.getMonth() + 1;
				let gDay = gd.getDate();

				calculateAge(gYear, gMonth, gDay);
			}
		});

		// 
		function calculateAge(year, month, day) {
			const today = new Date();
			let age = today.getFullYear() - year;
			if ((today.getMonth() + 1 < month) || (today.getMonth() + 1 === month && today.getDate() < day)) {
				age--;
			}
			$('#age').val(age);
			updatePassportOptions(age);
		}

		//  
		function updatePassportOptions(age) {
			if (age < 15) {
				$('#PassportType option').each(function () {
					$(this).toggle($(this).val() == "1").prop('selected', $(this).val() == "1");
				});
				$('#ProfessionId option').each(function () {
					$(this).toggle($(this).val() == "2").prop('selected', $(this).val() == "2");
				});
				$('#PaymentType option').each(function () {
					$(this).toggle($(this).val() == "1").prop('selected', $(this).val() == "1");
				});
				$('#PassportDuration option').each(function () {
					$(this).toggle($(this).val() == "1").prop('selected', $(this).val() == "1");
				});
				$('#AppointmentType option').each(function () {
					$(this).toggle($(this).val() == "1").prop('selected', $(this).val() == "1");
				});
				$('#FixedAmount').val("3,500 افغانی");
				$('#FixedPayAmount').show();
			} else {
				$('#PassportType option').show();
				$('#ProfessionId option').show();
				$('#PaymentType option').show();
				$('#PassportDuration option').show();
				$('#AppointmentType option').show();
				updateFixedAmount();
			}
		}

		//  
		function updateFixedAmount() {
			const passportType = $('#PassportType').val();
			const passportDuration = $('#PassportDuration').val();
			if (passportType == "2") {
				$('#FixedAmount').val(passportDuration == "1" ? "16,000 افغانی" : "31,000 افغانی");
			} else {
				$('#FixedAmount').val(passportDuration == "1" ? "5,500 افغانی" : "10,000 افغانی");
			}
			$('#FixedPayAmount').show();
		}

		$('#PassportType, #PassportDuration').on('change', updateFixedAmount);
	});



/*	@* --------------------Character validation-------------* @*/


		/*@* //profile image *@*/
		
			function previewImage(event) {
		var input = event.target;
			var preview = document.getElementById('profilePreview');

			if (input.files && input.files[0]) {
			var reader = new FileReader();

			reader.onload = function (e) {
				preview.src = e.target.result;
			preview.style.display = 'block';
			}

			reader.readAsDataURL(input.files[0]);
		}
	}
		
/*@* Profession Action with type of passport * @*/

	document.addEventListener("DOMContentLoaded", function () {
		const professionSelect = document.getElementById("ProfessionId");
		const passportTypeSelect = document.getElementById("PassportType");
		const commercialSection = document.getElementById("commercialPassportSection");

		const commercialProfessionId = "4";
		const commercialPassportTypeId = "2";

		function updateUI() {
			const selectedProfession = professionSelect.value;

			for (let i = 0; i < passportTypeSelect.options.length; i++) {
				const option = passportTypeSelect.options[i];

				if (option.value === "") {
					option.style.display = "block";
				} else if (selectedProfession === commercialProfessionId) {

					option.style.display = (option.value === commercialPassportTypeId) ? "block" : "none";
				} else {

					option.style.display = (option.value === commercialPassportTypeId) ? "none" : "block";


					if (passportTypeSelect.value === commercialPassportTypeId) {
						passportTypeSelect.value = "";
					}
				}
			}


			if (selectedProfession === commercialProfessionId) {
				commercialSection.style.display = "block";
				passportTypeSelect.value = commercialPassportTypeId;
			} else {
				commercialSection.style.display = "none";
			}
		}

		updateUI();

		professionSelect.addEventListener("change", updateUI);
	});

	/*@* --------------- * @*/

		//
		window.addEventListener("DOMContentLoaded", function () {
			const theme = localStorage.getItem("theme") || "light";

			if (theme === "dark") {
				document.body.classList.add("dark-mode");
			}
		});

	/*@* --------------- * @*/

		function showPreview() {
			const form = document.getElementById("registerForm");

			// 
			if (!form.checkValidity()) {
				alert("لطفاً تمام فیلدهای فورم را تکمیل نمایید.");
				form.reportValidity(); // 
				return;
			}

			// 
			buildAndShowPreview();
		}

function buildAndShowPreview() {
	const previewData = [];

	const photoInput = document.getElementById("ProfilePhoto");
	if (photoInput && photoInput.files && photoInput.files[0]) {
		const file = photoInput.files[0];
		const reader = new FileReader();

		reader.onload = function (e) {
			const photoHtml = `
		  <div class="col-12 mb-3 text-center">
			<img src="${e.target.result}" alt="عکس پروفایل"
			  style="max-width: 200px; max-height: 200px; border-radius: 0px; border: 1px solid #ccc; direction:right">
		  </div>`;

			fillPreviewContent(photoHtml);
		};

		reader.readAsDataURL(file);
	} else {
		fillPreviewContent('');
	}
}

function fillPreviewContent(photoHtml) {
	const previewData = [];

	const textFields = [
		{ label: "نام", id: "DName" },
		{ label: "نام ( انگلیسی )", id: "EName" },
		{ label: "تخلص", id: "DLastName" },
		{ label: "تخلص ( انگلیسی )", id: "ELastName" },
		{ label: "نام پدر", id: "DFatherName" },
		{ label: "نام پدر ( انگلیسی )", id: "EFatherName" },
		{ label: "نام پدرکلان", id: "DGrandFatherName" },
		{ label: "نام پدرکلان ( انگلیسی )", id: "EGrandFatherName" },
		{ label: "تاریخ تولد", id: "gregorianDate" },
		{ label: "قد", id: "Height" },
		{ label: "نمبر سند", id: "NIDNumber" },
	];

	textFields.forEach(field => {
		const value = document.getElementById(field.id)?.value || "---";
		previewData.push({ label: field.label, value: value });
	});

	const selectFields = [
		{ label: "سند", id: "IdentityDocumentTypeId" },
		{ label: "محل تولد", id: "SelectedBirthCityId" },
		{ label: "شغل", id: "ProfessionId" },
		{ label: "جنسیت", id: "GenderId" },
		{ label: "نوع پاسپورت", id: "PassportType" },
		{ label: "نوع درخواست", id: "AppointmentType" },
		{ label: "مدت پاسپورت", id: "PassportDuration" },
		{ label: "نوع پرداخت", id: "PaymentType" },
		{ label: "محل پروسس اسناد", id: "SelectedProcessingCityId" },
		{ label: "محل پروسس اسناد", id: "postalAddressSelect" },
	];

	selectFields.forEach(field => {
		const element = document.getElementById(field.id);
		const selectedText = element?.options[element.selectedIndex]?.text || "---";
		previewData.push({ label: field.label, value: selectedText });
	});

	const additionalTextFields = [
		{ label: "جزئیات آدرس", id: "AddressDetails" },
		{ label: "نمبر تماس اصلی", id: "PrimaryPhone" },
		{ label: "نمبر تماس فرعی", id: "SecondaryPhone" },
	];

	additionalTextFields.forEach(field => {
		const value = document.getElementById(field.id)?.value || "---";
		previewData.push({ label: field.label, value: value });
	});

	let previewHtml = photoHtml;
	previewData.forEach(item => {
		previewHtml += `
		<div class="col-md-6">
		  <div class="card h-100 shadow-sm rounded-3">
			<div class="card-body">
			  <h6 class="card-title  mb-1">${item.label}</h6><br/>
			  <p class="card-text fw-bold">${item.value}</p>
			</div>
		  </div>
		</div>`;
	});

	document.getElementById("previewContent").innerHTML = previewHtml;

	const modal = new bootstrap.Modal(document.getElementById('dataPreviewModal'));
	modal.show();
}
//---------------------------------------------
document.querySelectorAll('.force-english').forEach(input => {
	input.addEventListener('keydown', function (e) {
		const code = e.code;

		const allowedKeys = [
			'Backspace', 'Tab', 'Enter', 'Escape',
			'ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown',
			'Delete', 'Home', 'End', 'Control', 'Shift',
			'CapsLock', 'Alt', 'Meta', 'Space'  // اضافه کردن 'Space' برای فاصله
		];

		if (e.ctrlKey || e.metaKey || allowedKeys.includes(code)) return;

		// اگر کلید space بود، اجازه بده مستقیم
		if (code === 'Space') return;

		const enChar = getEnglishCharFromKeyCode(code, e.shiftKey);
		if (!enChar) {
			e.preventDefault();
			return;
		}

		if (this.value.length >= 25 && this.selectionStart === this.selectionEnd) {
			e.preventDefault();
			return;
		}

		e.preventDefault();

		const start = this.selectionStart;
		const end = this.selectionEnd;
		const currentValue = this.value;
		this.value = (
			currentValue.substring(0, start) +
			enChar.toUpperCase() +
			currentValue.substring(end)
		);
		this.setSelectionRange(start + 1, start + 1);
	});
});

function getEnglishCharFromKeyCode(code, shift) {
	const keyMap = {
		KeyA: 'a', KeyB: 'b', KeyC: 'c', KeyD: 'd',
		KeyE: 'e', KeyF: 'f', KeyG: 'g', KeyH: 'h',
		KeyI: 'i', KeyJ: 'j', KeyK: 'k', KeyL: 'l',
		KeyM: 'm', KeyN: 'n', KeyO: 'o', KeyP: 'p',
		KeyQ: 'q', KeyR: 'r', KeyS: 's', KeyT: 't',
		KeyU: 'u', KeyV: 'v', KeyW: 'w', KeyX: 'x',
		KeyY: 'y', KeyZ: 'z'
	};

	const char = keyMap[code];
	return shift ? char?.toUpperCase() : char;
}

	//-----------------------------------------




	document.querySelectorAll('.force-persian').forEach(input => {
		input.addEventListener('keydown', function (e) {
			const allowedControlKeys = [
				'Backspace', 'Tab', 'Enter', 'Escape',
				'ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown',
				'Delete', 'Home', 'End'
			];

			
			if (e.ctrlKey || e.metaKey || allowedControlKeys.includes(e.key)) return;

			e.preventDefault(); 

			const faChar = getPersianCharFromKeyCode(e.code, e.shiftKey);
			if (faChar && isOnlyPersianLetter(faChar)) {
				const start = this.selectionStart;
				const end = this.selectionEnd;
				const currentValue = this.value;
				 const newLength = this.value.length - (this.selectionEnd - this.selectionStart) + 1;
		if (newLength > 30 && !isReplacing) return;
				this.value = currentValue.substring(0, start) + faChar + currentValue.substring(end);
				this.setSelectionRange(start + 1, start + 1);
			}
		});
	});

	
	function getPersianCharFromKeyCode(code, shift) {
		const map = {
			'KeyQ': 'ض', 'KeyW': 'ص', 'KeyE': 'ث', 'KeyR': 'ق', 'KeyT': 'ف',
			'KeyY': 'غ', 'KeyU': 'ع', 'KeyI': 'ه', 'KeyO': 'خ', 'KeyP': 'ح',
			'BracketLeft': 'ج', 'BracketRight': 'چ',
			'KeyA': 'ش', 'KeyS': 'س', 'KeyD': 'ی', 'KeyF': 'ب', 'KeyG': 'ل',
			'KeyH': 'ا', 'KeyJ': 'ت', 'KeyK': 'ن', 'KeyL': 'م',
			'Semicolon': 'ک', 'Quote': 'گ',
			'KeyZ': 'ظ', 'KeyX': 'ط', 'KeyC': 'ز', 'KeyV': 'ر', 'KeyB': 'ذ',
			'KeyN': 'د', 'KeyM': 'پ', 'Comma': 'و', 'Period': '؟',
			'Space': ' '
		};
		return map[code] || null;
	}

	
	function isOnlyPersianLetter(char) {
		const persianRegex = /^[\u0600-\u06FF\s‌‍]+$/;
		return persianRegex.test(char);
	}
//--------------------------------
		input.addEventListener('input', function() {
		if (this.value.length > 30) {
			this.value = this.value.slice(0, 30);
		}
	});



	// dark mode js
	document.addEventListener('DOMContentLoaded', function () {
	  const input = document.getElementById('shamsiDate');
	  function toggleDarkBg() {
		if (input.value.trim() !== '') {
		  input.classList.add('filled');
		} else {
		  input.classList.remove('filled');
		}
	  }
	  input.addEventListener('input', toggleDarkBg);
	  toggleDarkBg(); 
	});

		document.addEventListener('DOMContentLoaded', function () {
	  const inputs = document.querySelectorAll('input.observer-example');

	  inputs.forEach(input => {
		function toggleDarkBg() {
		  if (input.value.trim() !== '') {
			input.classList.add('filled');
		  } else {
			input.classList.remove('filled');
		  }
		}

		input.addEventListener('input', toggleDarkBg);
		input.addEventListener('blur', toggleDarkBg);

		toggleDarkBg();

		setTimeout(toggleDarkBg, 500);
	  });
	});
	// end dark mode js


//Post address

//End post address


