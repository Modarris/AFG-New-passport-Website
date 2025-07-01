
const persianToEnglishMap = {
    'ض': 'q', 'ص': 'w', 'ث': 'e', 'ق': 'r', 'ف': 't', 'غ': 'y', 'ع': 'u', 'ه': 'i', 'خ': 'o', 'ح': 'p',
    'ج': '{', 'چ': '}', 'ش': 'a', 'س': 's', 'ی': 'd', 'ب': 'f', 'ل': 'g', 'ا': 'h', 'ت': 'j',
    'ن': 'k', 'م': 'l', 'ک': ';', 'گ': '"', 'ظ': 'z', 'ط': 'x', 'ز': 'c', 'ر': 'v', 'ذ‌': 'b', 'د': 'n', 'پ': 'm', 'و': ','
};


const persianToEnglishMapUpper = {
    'ض': 'Q', 'ص': 'W', 'ث': 'E', 'ق': 'R', 'ف': 'T', 'غ': 'Y', 'ع': 'U', 'ه': 'I', 'خ': 'O', 'ح': 'P',
    'ج': '{', 'چ': '}', 'ش': 'A', 'س': 'S', 'ی': 'D', 'ب': 'F', 'ل': 'G', 'ا': 'H', 'ت': 'J',
    'ن': 'K', 'م': 'L', 'ک': ';', 'گ': '"', 'ظ': 'Z', 'ط': 'X', 'ز': 'C', 'ر': 'V', 'ذ‌': 'B', 'د': 'N', 'پ': 'M', 'و': ','
    };

    const allowedKeys = ['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight', 'Home', 'End'];

    document.querySelectorAll('#englishfield').forEach(function (input) {
        input.addEventListener('keydown', function (e) {
            const key = e.key;

          
            if (allowedKeys.includes(key)) return;

            let translatedChar = '';

           
            const isUpper = e.getModifierState("CapsLock") || e.shiftKey;

            if (isUpper && persianToEnglishMapUpper[key]) {
                translatedChar = persianToEnglishMapUpper[key];
            } else if (persianToEnglishMap[key]) {
                translatedChar = persianToEnglishMap[key];
            }

          
            if (translatedChar) {
                e.preventDefault();
                const cursorPos = this.selectionStart;
                const before = this.value.substring(0, cursorPos);
                const after = this.value.substring(this.selectionEnd);
                this.value = before + translatedChar + after;
                this.setSelectionRange(cursorPos + 1, cursorPos + 1);
            }
            else if (/^[a-zA-Z ]$/.test(key)) {
              
            }
            else {
                e.preventDefault(); 
            }
        });
    });

   
    document.querySelectorAll('#persianfield').forEach(function (input) {
        input.addEventListener('keydown', function (e) {
            const key = e.key;

         
            if (key === 'Backspace' || key === 'Delete' || key === 'ArrowLeft' || key === 'ArrowRight' || key === 'Tab') {
                return;  
            }

        
            const isEnglishKey = /^[a-zA-Z]$/.test(key);
            if (isEnglishKey) {
                e.preventDefault();
            }
        });
      });
