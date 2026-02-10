//window.onload = (e) => {
//    for (let i = 0; i < [document.querySelectorAll('.datepickerteamwork')][0].length; i++) {
//        ([document.querySelectorAll('.datepickerteamwork')][0])[i].setAttribute("readonly", "readonly");
//    }
//}

handleKeyDownDatepicker = (e) => {
    handleValidateTextDatepicker(e);
};

handleValidateTextDatepicker = (e) => {
    const formatDate = /[0-9]{2}-[0-9]{2}-[0-9]{4}/;
    const targetError = document.getElementById(e.dataset.targeterror);

    if (!formatDate.exec(e.value)) {
        e.style.borderBottom = "1px solid rgb(255, 0, 0)";
        e.style.color = "rgb(255, 0, 0)";
        targetError.innerHTML = 'El formato de la fecha esta incorrecto (Formato: DD-MM-AAAA)';
        targetError.style.color = "rgb(255, 0, 0)";

        e.dataset.day = "";
        e.dataset.month = "";
        e.dataset.year = "";
    }
    else
    {
        let validate = true;
        const arrayFecha = [...e.value.split('-')];

        if ((Number(arrayFecha[2]) < 1900) && validate) {
            validate = false;
        }

        if ((Number(arrayFecha[1]) < 1 || Number(arrayFecha[1]) > 12) && validate) {
            validate = false;
        }

        if (validate) {
            const lastDayFecha = new Date(Number(arrayFecha[2]), Number(arrayFecha[1]), 0).getDate();

            if (Number(arrayFecha[0]) < 1 || Number(arrayFecha[0]) > lastDayFecha) {
                validate = false;
            }
        }

        if (validate && e.value.length === 10) {
            e.style.borderBottom = "1px solid rgb(200, 200, 200)";
            e.style.color = "rgb(100, 100, 100)";
            targetError.innerHTML = '';
            targetError.style.color = "rgb(100, 100, 100)";

            e.dataset.day = Number(arrayFecha[0]);
            e.dataset.month = Number(arrayFecha[1]) - 1;
            e.dataset.year = Number(arrayFecha[2]);

            e.click();
        }
        else {
            e.style.borderBottom = "1px solid rgb(255, 0, 0)";
            e.style.color = "rgb(255, 0, 0)";
            targetError.innerHTML = 'El fecha no es valida, indique una fecha real.';
            targetError.style.color = "rgb(255, 0, 0)";

            e.dataset.day = "";
            e.dataset.month = "";
            e.dataset.year = "";

        }

    }
};

document.querySelector('html').addEventListener('click', (e) => {

    let show = false;
    let showLegend = true;

    [...e.target.classList].map(x => {
        if (x === "datepickerteamwork") {
            show = true;
        }

        if (x === "datepicker__input" || x === "datepicker__legend") {
            showLegend = false;
        }
    });

    if (!show) {
        for (let i = 0; i < [document.querySelectorAll('.datepicker-teamwork')][0].length; i++) {
            ([document.querySelectorAll('.datepicker-teamwork')][0])[i].innerHTML = '';
        }
    }

    /** Hidden => para legendas de datepicker */
    if (showLegend) {
        for (let i = 0; i < [document.querySelectorAll('.datepicker__legend')][0].length; i++) {
            const input = ([document.querySelectorAll('.datepicker__legend')][0])[i].attributes.for.nodeValue;
            if (document.getElementById(input).value === "") {
                ([document.querySelectorAll('.datepicker__legend')][0])[i].style.transform = "translate3d(0, 0, 0)";
                ([document.querySelectorAll('.datepicker__legend')][0])[i].style.transition = "all .15s ease-out";
            }
        }
    }
    
});

handleBlurDatepicker = (e) => {
    let show = false;

    [...e.classList].map(x => {
        if (x === "datepickerteamwork") {
            show = true;
        }
    });

    if (!show) {
        for (let i = 0; i < [document.querySelectorAll('.datepicker-teamwork')][0].length; i++) {
            ([document.querySelectorAll('.datepicker-teamwork')][0])[i].innerHTML = '';
        }
    }

    /** Hidden => para legendas de datepicker */
    for (let i = 0; i < [document.querySelectorAll('.datepicker__legend')][0].length; i++) {
        const input = ([document.querySelectorAll('.datepicker__legend')][0])[i].attributes.for.nodeValue;
        if (document.getElementById(input).value === "") {
            ([document.querySelectorAll('.datepicker__legend')][0])[i].style.transform = "translate3d(0, 0, 0)";
            ([document.querySelectorAll('.datepicker__legend')][0])[i].style.transition = "all .15s ease-out";
        }
        
    }
};

handleClickDatepickerLegend = (e) => {
    document.querySelector(`#${e.attributes.for.nodeValue}`).click();
    e.style.transform = "translate3d(0, -30px, 0)";
    e.style.transition = "all .15s ease-out";
};

handleClickChangeViewDatepickerYear = (e) => {
    e.innerHTML = `${Number(e.dataset.year) - 7} - ${Number(e.dataset.year) + 7} ▾`;

    const years = [];

    for (let i = Number(e.dataset.year) - 7; i < Number(e.dataset.year); i++) {
        years.push(i);
    }

    years.push(Number(e.dataset.year));

    for (let j = Number(e.dataset.year) + 1; j < Number(e.dataset.year) + 8; j++) {
        years.push(j);
    }

    let yearsHTML = '';

    let row = 1;

    for (let x = 0; x < years.length; x++) {
        if (row === 1) {
            yearsHTML = yearsHTML + `<tr>`;
        }

        if (row < 4 && row > 0) {
            yearsHTML = yearsHTML + `<td class="ta-center new-family-teamwork pdt-10 pdb-10 datepickerteamwork">
                                            <span class="datepickerteamwork" data-datepicker="${e.dataset.datepicker}" onclick="handleClickChangeViewDatepickerMonth(this)" data-day="" data-month="" data-year="${years[x]}">${years[x]}</span>
                                         </td>`;
        }

        if (row === 3) {
            yearsHTML = yearsHTML + `</tr>`;
            row = 0;
        }

        row = row + 1;
    }

    document.querySelector(`#${e.dataset.datepicker} .body-datepicker table tbody`).innerHTML =
        `<tr>
                <td colspan="7">
                    <table>
                        <tbody>${yearsHTML}</tbody>
                    </table>
                </td>
            </td>`;

    e.setAttribute("onclick", "");

}

handleClickChangeViewDatepickerMonth = (e) => {
    document.querySelector(`#${e.dataset.datepicker} .body-datepicker table thead .action__selectedtype__datepicker`).innerHTML = `${e.dataset.year} ▾`;

    const meses = ["ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"];

    let mesesHTML = '';

    let row = 1;

    for (let i = 0; i < meses.length; i++) {
        if (row === 1) {
            mesesHTML = mesesHTML + `<tr>`;
        }

        if (row < 5 && row > 0) {
            mesesHTML = mesesHTML + `<td class="ta-center new-family-teamwork pdt-10 pdb-10 datepickerteamwork">
                                            <span class="datepickerteamwork" data-legend="${e.dataset.datepicker}-legend" data-datepicker="${e.dataset.datepicker}" onclick="handleClickDatePicker(this)" data-day="" data-month="${i}" data-year="${e.dataset.year}">${meses[i]}</span>
                                         </td>`;
        }

        if (row === 4) {
            mesesHTML = mesesHTML + `</tr>`;
            row = 0;
        }

        row = row + 1;
    }

    document.querySelector(`#${e.dataset.datepicker} .body-datepicker table tbody`).innerHTML =
        `<tr>
                <td colspan="7">
                    <table>
                        <tbody>${mesesHTML}</tbody>
                    </table>
                </td>
            </td>`;

    for (let i = 0; i < [document.querySelectorAll(`#${e.dataset.datepicker} .body-datepicker table thead .item__days__header`)][0].length; i++) {
        ([document.querySelectorAll(`#${e.dataset.datepicker} .body-datepicker table thead .item__days__header`)][0])[i].innerHTML = '';
    }

    document.querySelector(`#${e.dataset.datepicker} .body-datepicker table thead .action__selectedtype__datepicker`).setAttribute("onclick", "handleClickChangeViewDatepickerYear(this)");
};

handleClickSelectedDayDatepicker = (e) => {
    document.getElementById(`${e.dataset.target}-input`).value = `${(Number(e.dataset.day) >= 10) ? e.dataset.day : `0${e.dataset.day}`}-${(Number(e.dataset.month) >= 10) ? e.dataset.month : `0${e.dataset.month}`}-${e.dataset.year}`;
    document.getElementById(`${e.dataset.target}-input`).dataset.month = Number(e.dataset.month) - 1;
    document.getElementById(`${e.dataset.target}-input`).dataset.year = e.dataset.year;
    document.getElementById(`${e.dataset.target}-input`).dataset.day = e.dataset.day;

    for (let i = 0; i < [document.querySelectorAll('.item__days__datepicker')][0].length; i++) {
        [...([document.querySelectorAll('.item__days__datepicker')][0])[i].classList].map(x => {
            if (x === "teamwork" || x === "color-fx3") {
                ([document.querySelectorAll('.item__days__datepicker')][0])[i].classList.remove('teamwork', "color-fx3");
            }
        });
    }

    e.classList.add("teamwork", "color-fx3");
    handleValidateTextDatepicker(document.getElementById(`${e.dataset.target}-input`));
};

handleClickDatePicker = (e) => {
    handleClickDatepickerLegend(document.getElementById(e.dataset.legend));

    let daysHTML = '';

    for (let i = 0; i < [document.querySelectorAll('.datepicker-teamwork')][0].length; i++) {
        ([document.querySelectorAll('.datepicker-teamwork')][0])[i].innerHTML = '';
    }

    const daySelected = e.dataset.day;

    const date = (e.dataset.month === "" && e.dataset.year === "") ? new Date(new Date().getFullYear(), new Date().getMonth()) : new Date(Number(e.dataset.year), Number(e.dataset.month));
    const datepicker = e.dataset.datepicker;

    const monthActual = date.getMonth();
    const yearActual = date.getFullYear();

    const dayfinal = new Date(yearActual, monthActual + 1, 0).getDate();

    const dateNext = new Date(yearActual, monthActual + 2, 0);

    const datePrev = new Date(yearActual, monthActual, 0);

    const dayInicio = (date.getDay() !== 0) ? date.getDay() : 7;

    const meses = ["ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"];

    const monthYear = meses[monthActual];

    let row = 1;

    for (let i = 1; i <= Number(dayfinal) + Number(dayInicio) - 1; i++) {
        if (row === 1) {
            daysHTML = daysHTML + `<tr>`;
        }

        if (row < 8 && row > 0) {
            daysHTML = daysHTML +
                ((Number(dayInicio) <= i)
                    ? `<td class="ta-center new-family-teamwork pdt-10 pdb-10 datepickerteamwork"><span onclick="handleClickSelectedDayDatepicker(this)" data-legend="${datepicker}-legend"
                           class="datepickerteamwork item__days__datepicker ${(daySelected !== "") ? (Number(daySelected) === (i - Number(dayInicio) + 1)) ? `teamwork color-fx3` : `` : ``}"
                            data-target="${datepicker}" data-day="${i - Number(dayInicio) + 1}" data-month="${monthActual + 1}"
                            data-year="${yearActual}">${i - Number(dayInicio) + 1}</span></td>`
                    : `<td class="ta-center new-family-teamwork pdt-10 pdb-10"></td>`);
        }

        if (row === 7) {
            daysHTML = daysHTML + `</tr>`;
            row = 0;
        }

        row = row + 1;
    }

    document.getElementById(datepicker).innerHTML =
        `
                <div class="body-datepicker">
                    <table class="datepickerteamwork">
                        <thead>
                            <tr class="head-month-year">
                                <td colspan="5" class="pdl-20 pdb-20 pdt-20 new-family-teamwork month-year cursor-pointer">
                                    <span class="datepickerteamwork action__selectedtype__datepicker" data-datepicker="${datepicker}" data-legend="${datepicker}-legend" onclick="handleClickChangeViewDatepickerMonth(this)" data-type="month" data-day="" data-month="${monthActual + 1}" data-year="${yearActual}">${monthYear} ${yearActual} &#x25BE;</span>
                                </td>
                                <td class="ta-center new-family-teamwork cursor-pointer datepickerteamwork action-prev-datepicker" data-legend="${datepicker}-legend" data-datepicker="${datepicker}" onclick="handleClickDatePicker(this)" data-day="" data-month="${datePrev.getMonth()}" data-year="${datePrev.getFullYear()}">&#x25C0;</td>
                                <td class="ta-center new-family-teamwork cursor-pointer datepickerteamwork action-next-datepicker" data-legend="${datepicker}-legend" data-datepicker="${datepicker}" onclick="handleClickDatePicker(this)" data-day="" data-month="${dateNext.getMonth()}" data-year="${dateNext.getFullYear()}">&#x25B6;</td>
                            </tr>
                            <tr class="head-days">
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">L</td>
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">M</td>
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">MI</td>
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">J</td>
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">V</td>
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">S</td>
                                <td class="ta-center new-family-teamwork datepickerteamwork item__days__header">D</td>
                            </tr>
                        </thead>
                        <tbody>
                            ${daysHTML}
                        </tbody>
                    </table>
                </div>

            `;
};