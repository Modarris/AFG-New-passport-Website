

    const yearSelect = document.getElementById("year");
    for (let i = 1405; i >= 1300; i--) {
        yearSelect.add(new Option(i, i));
    }

    const monthSelect = document.getElementById("month");
    const afghanMonths = ["حمل", "ثور", "جوزا", "سرطان", "اسد", "سنبله", "میزان", "عقرب", "قوس", "جدی", "دلو", "حوت"];
    for (let i = 1; i <= 12; i++) {
        monthSelect.add(new Option(afghanMonths[i - 1], i));
    }

    function updateDays() {
      const year = parseInt(yearSelect.value);
    const month = parseInt(monthSelect.value);
    const daysInMonth = jalaali.jalaaliMonthLength(year, month);
    const daySelect = document.getElementById("day");
    daySelect.innerHTML = "";
    for (let i = 1; i <= daysInMonth; i++) {
        daySelect.add(new Option(i, i));
      }
    convertDate();
    }

    yearSelect.addEventListener("change", updateDays);
    monthSelect.addEventListener("change", updateDays);
    document.getElementById("day").addEventListener("change", convertDate);
    updateDays();

    function convertDate() {
      const jYear = parseInt(yearSelect.value);
    const jMonth = parseInt(monthSelect.value);
    const jDay = parseInt(document.getElementById("day").value);
    if (!jYear || !jMonth || !jDay) {
        document.getElementById('gregorianDate').value = "لطفا تاریخ را انتخاب کنید";
    document.getElementById('age').value = "";
    return;
      }

    const gregorian = jalaali.toGregorian(jYear, jMonth, jDay);
    const gregorianDate = `${gregorian.gy}/${gregorian.gm}/${gregorian.gd}`;
    document.getElementById('gregorianDate').value = gregorianDate;

    calculateAge(gregorian.gy, gregorian.gm, gregorian.gd);
    }

    function calculateAge(year, month, day) {
      const today = new Date();
    let age = today.getFullYear() - year;

    if (today.getMonth() + 1 < month || (today.getMonth() + 1 === month && today.getDate() < day)) {
        age--;
      }

    document.getElementById('age').value = `${age} سال`;
    }
