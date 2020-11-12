// angular-ac code

var th = ['', 'thousand', 'million', 'billion', 'trillion'];
var dg = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
var dga = ['initial', 'first', 'second', 'third', 'fourth', 'fifth', 'sixth', 'seventh', 'eighth', 'ninth'];
var tn = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
var tw = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];


$.fn.setCursorPosition = function (pos) {
    this.each(function (index, elem) {
        if (elem.setSelectionRange) {
            elem.setSelectionRange(pos, pos);
        } else if (elem.createTextRange) {
            var range = elem.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    });
    return this;
};


//function toWords(s) {
//    s = s.toString();
//    s = s.replace(/[\, ]/g, '');
//    if (s !== parseFloat(s)) return 'not a number';
//    var x = s.indexOf('.');
//    if (x === -1) x = s.length;
//    if (x > 15) return 'too big';
//    var n = s.split('');
//    var str = '';
//    var sk = 0;
//    for (var i = 0; i < x; i++) {
//        if ((x - i) % 3 === 2) {
//            if (n[i] === '1') {
//                str += tn[Number(n[i + 1])] + ' ';
//                i++;
//                sk = 1;
//            }
//            else if (n[i] !== 0) {
//                str += tw[n[i] - 2] + ' ';
//                sk = 1;
//            }
//        }
//        else if (n[i] !== 0) {
//            str += dg[n[i]] + ' ';
//            if ((x - i) % 3 === 0) str += 'hundred ';
//            sk = 1;
//        }


//        if ((x - i) % 3 === 1) {
//            if (sk) str += th[(x - i - 1) / 3] + ' ';
//            sk = 0;
//        }
//    }
//    if (x !== s.length) {
//        var y = s.length;
//        str += 'point ';
//        for (var i = x + 1; i < y; i++) str += dg[n[i]] + ' ';
//    }
//    return str.replace(/\s+/g, ' ');
//}
//function toWordsR(s) {
//    s = s.toString();
//    s = s.replace(/[\, ]/g, '');
//    if (s !== parseFloat(s)) return 'not a number';
//    var x = s.indexOf('.');
//    if (x === -1) x = s.length;
//    if (x > 15) return 'too big';
//    var n = s.split('');
//    var str = '';
//    var sk = 0;
//    for (var i = 0; i < x; i++) {
//        if ((x - i) % 3 == 2) {
//            if (n[i] == '1') {
//                str += tn[Number(n[i + 1])] + ' ';
//                i++;
//                sk = 1;
//            }
//            else if (n[i] != 0) {
//                str += tw[n[i] - 2] + ' ';
//                sk = 1;
//            }
//        }
//        else if (n[i] != 0) {
//            str += dga[n[i]] + ' ';
//            if ((x - i) % 3 == 0) str += 'hundred ';
//            sk = 1;
//        }


//        if ((x - i) % 3 == 1) {
//            if (sk) str += th[(x - i - 1) / 3] + ' ';
//            sk = 0;
//        }
//    }
//    if (x != s.length) {
//        var y = s.length;
//        str += 'point ';
//        for (var i = x + 1; i < y; i++) str += dga[n[i]] + ' ';
//    }
//    return str.replace(/\s+/g, ' ');
//}
//window.toWords = toWords;


if (typeof addLoadEvent === "undefined") {
    var addLoadEvent = function (func) {
        var oldonload = window.onload;
        if (typeof window.onload !== 'function') {
            window.onload = func;
        }
        else {
            window.onload = function () { oldonload(); func(); }
        }
    }
}
function openNav() {
    document.getElementById("mySidenav").style.width = "350px";
    document.getElementById("main").style.marginLeft = "350px";
    $("#mySidenav").children('ul').children('li').children('a').children('span').css('display', "inline-block");
    //$("#mySidenav").children('form').css('visibility', "visible");
    $("#mySidenav").children('h4').css('visibility', "visible");
    $("#OpenLink").css('display', "none");
    $("#CloseLink").css('display', "block");
}
function closeNav() {
    document.getElementById("mySidenav").style.width = "80px";
    document.getElementById("main").style.marginLeft = "80px";
    $("#mySidenav").children('ul').children('li').children('a').children('span').css('display', "none");
    //$("#mySidenav").children('form').css('visibility', "hidden");
    $("#mySidenav").children('h4').css('visibility', "hidden");
    $("#OpenLink").css('display', "block");
    $("#CloseLink").css('display', "none");
}
function toggleNav() {
    if ($("#mySidenav").css('width') === "80px") {
        openNav();
    } else {
        closeNav();
    }
}

// bootstrap function for smooth scrolling
$(document).ready(function () {
    // Add smooth scrolling to all links in navbar + footer link
    $(".navbar a, footer a[href='#myPage']").on('click', function (event) {

        // Make sure this.hash has a value before overriding default behavior
        if (this.hash !== "") {

            // Prevent default anchor click behavior
            event.preventDefault();

            // Store hash
            var hash = this.hash;

            // Using jQuery's animate() method to add smooth page scroll
            // The optional number (900) specifies the number of milliseconds it takes to scroll to the specified area
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 900, function () {

                // Add hash (#) to URL when done scrolling (default click behavior)
                window.location.hash = hash;
            });
        } // End if
    });
})

$(function () {
    $('.date').datetimepicker({
        //defaultDate: new Date(),
        format: 'dddd, DD MMMM, YYYY, hh:mm A',
        sideBySide: true,
        allowInputToggle: true
    });
});
$(function () {
    $('.date-only').datetimepicker({
        format: 'dddd, DD MMMM, YYYY'
    });
});
$(function () {
    $('.date-not-today').datetimepicker({
        //defaultDate: new Date(),
        format: 'dddd, DD MMMM, YYYY, hh:mm A',
        sideBySide: true,
        allowInputToggle: true,
        disabledDates: [
            new Date()
        ]
    });
});
$(function () {
    $('.date-today-only').datetimepicker({
        //defaultDate: new Date(),
        format: 'dddd, DD MMMM, YYYY, hh:mm A',
        //format:'LT',
        sideBySide: true,
        allowInputToggle: true,
        enabledDates: [new Date()]
    });
});
$(function () {
    $('.time-only').datetimepicker({
        //defaultDate: new Date(),
        format: 'hh:mm A',
        sideBySide: true,
        allowInputToggle: true,
        enabledDates: [new Date()]
    });
});
$(function GetDayById(id) {
    var weekday = new Array(7);
    weekday[0] = "Sunday";
    weekday[1] = "Monday";
    weekday[2] = "Tuesday";
    weekday[3] = "Wednesday";
    weekday[4] = "Thursday";
    weekday[5] = "Friday";
    weekday[6] = "Saturday";
    return weekday[(id - 1)];
});


var MMSAngularJSApp = angular.module('MMSAngularJSApp', ['angular.filter']);
MMSAngularJSApp.filter('words', function () {
    function isInteger(x) {
        return x % 1 === 0;
    }

    return function (value) {
        if (value && isInteger(value))
            return toWords(value);

        return value;
    };

});
MMSAngularJSApp.filter('wordsR', function () {
    function isInteger(x) {
        return x % 1 === 0;
    }

    return function (value) {
        if (value && isInteger(value))
            return toWordsR(value);

        return value;
    };

});

MMSAngularJSApp.service('UtilityService', function ($http) {
    this.GetUnixMiliSec = function (inputdate, hr, min, ampm) {
        var date = new Date(inputdate);
        var unixmilisec = 0;
        var hrreviewed = new Number(hr);
        if (ampm === "PM" && hrreviewed < 12)
            hrreviewed += 12;
        if (ampm === "AM" && hrreviewed === 12)
            hrreviewed -= 12;
        date.setHours(hrreviewed);
        date.setMinutes(new Number(min));
        unixmilisec = date.getTime() + ((date.getTimezoneOffset() / -60) * 60 * 60 * 1000);

        return unixmilisec;
    },
        this.GetUnixMiliSecDate = function (inputdate) {
            var date = new Date(inputdate);
            var unixmilisec = 0;
            unixmilisec = date.getTime() + ((date.getTimezoneOffset() / -60) * 60 * 60 * 1000);
            return unixmilisec;
        },
        this.GetDate = function (inputdate, hr, min, ampm) {
            var date = new Date(inputdate);
            var unixmilisec = 0;
            var hrreviewed = new Number(hr);
            if (ampm === "PM" && hrreviewed < 12) hrreviewed = hrreviewed + 12;
            if (ampm === "AM" && hrreviewed === 12) hrreviewed = hrreviewed - 12;
            date.setHours(hrreviewed);
            date.setMinutes(new Number(min));
            return date;
        },
        this.UpdateFormValidationDataOnSuccess = function (formElement, response) {
            if (response.status === 200 && response.data.status === 0) {
                formElement.parent('div').prepend('<div id="success" class="alert alert-success rounded-0"><strong> Success </strong> <br><small>Reloading Page ...</small></div>');
                formElement.remove();
                setInterval(function () { location.reload(); }, 1000);

            } else {
                $.each(response.data.errorList, function (index, value) {
                    if (value.field !== null && value.field !== '' && formElement.find('#'.concat(value.field)).length > 0) {
                        formElement.find('#'.concat(value.field)).closest('.form-group').children('.form-control-feedback').text(value.message);
                        formElement.find('#'.concat(value.field)).addClass('is-invalid');
                        formElement.find('#'.concat(value.field)).closest('.form-group').children('.form-control-feedback').addClass('invalid-feedback');
                    } else {
                        formElement.prepend('<div id="error" class="alert alert-danger rounded-0"><strong> ' + value.field + ': ' + value.message + ' </strong></div>');
                    }
                });
                $.each(response.data.warningList, function (index, value) {
                    if (value.field !== null && value.field !== '' && formElement.find('#'.concat(value.field)).length > 0) {
                        formElement.find('#'.concat(value.field)).closest('.form-group').children('.form-control-warning').text(value.message);
                        formElement.find('#'.concat(value.field)).closest('.form-group').children('.form-control-warning').addClass('text-warning');
                    } else {
                        formElement.prepend('<div id="warning" class="alert alert-warning rounded-0"><strong> ' + value.field + ': ' + value.message + ' </strong></div>');
                    }
                });
            }
        },
        this.UpdateFormValidationDataOnError = function (formElement, response) {
            formElement.prepend('<div id="error" class="alert alert-danger rounded-0"><strong> Error code: ' + response.status + ' </strong></div>');
        },
        this.RemoveFormElementValidationData = function (formElement) {
            formElement.find("div[id=error]").remove();
            formElement.find("div[id=warning]").remove();
            formElement.find("div[id=success]").remove();
            formElement.find('.form-control-feedback').removeClass('invalid-feedback');
            formElement.find('.form-control-feedback').text('');
            formElement.find('input').removeClass('is-invalid');
            formElement.find('select').removeClass('is-invalid');
            formElement.find('.form-control-warning').text('');
            formElement.find('.form-control-warning').removeClass('text-warning');
        },
        this.GetDayById = function GetDay(id) {
            var weekday = new Array(7);
            weekday[0] = "Saturday";
            weekday[1] = "Sunday";
            weekday[2] = "Monday";
            weekday[3] = "Tuesday";
            weekday[4] = "Wednesday";
            weekday[5] = "Thursday";
            weekday[6] = "Friday";
            return weekday[(id - 1)];
        },
        this.GetJsonDataFromAPI = function (url) {
            fetch(url)
                .then(result => { return result.json(); })
                .then(data => {
                    return data;
                });
        }
});

MMSAngularJSApp.service('FormService', function ($http, UtilityService) {
    this.FormSubmit = function (SubmitURL, e, Model) {
        var formElement = angular.element(e.target);
        UtilityService.RemoveFormElementValidationData(formElement);
        $http({
            method: "POST",
            data: JSON.stringify(Model),
            url: SubmitURL
        }).then(
            function mySuccess(response) {
                UtilityService.UpdateFormValidationDataOnSuccess(formElement, response);
            },
            function myError(response) {
                UtilityService.UpdateFormValidationDataOnError(formElement, response);
            });
    },
        this.GridDataSubmit = function (SubmitURL, msgcontainerid, Model) {

            $http({
                method: "POST",
                data: JSON.stringify(Model),
                url: SubmitURL
            }).then(
                function mySuccess(response) {
                    $('#' + msgcontainerid).find('#msgstatus').html('<span class="text-success">success</span>');

                    return response.data.data.id;
                },
                function myError(response) {
                    $('#' + msgcontainerid).find('#msgstatus').html('<span class="text-error">error !</span>');
                    return response.data.data.id;
                });
        }
});
MMSAngularJSApp.factory('HttpDataService', function ($http) {
    return {
        GetData: function (SubmitURL) {
            return $http.get(SubmitURL).then(function (response) {
                return response.data;
            });
        }
    };
});


AMSAngularJSApp.controller('CtrSoreInItem', function ($http, $scope) {
    $scope.academicSessionList = "";
    $http.get('/api/admin/dashboard/GetSoreInItemList').then(function (response) { $scope.academicSessionList = response.data; });
});
AMSAngularJSApp.controller('CtrStoreInItemForm', function ($http, $scope, UtilityService) {
    $scope.acaCalendar = "";
    $scope.acaCalUnit = "";
    $scope.acaYear = "";
    $scope.sequence = 0;
    $scope.name = "";
    $scope.code = "";
    $scope.startDate = "";
    $scope.endDate = "";

    $scope.acaCalendarList = "";
    $scope.acaCalUnitList = "";
    $scope.acaYearList = "";
    $http.get('/api/ddlhelper/GetCalendars').then(function (response) { $scope.acaCalendarList = response.data; });
    $http.get('/api/ddlhelper/GetUnits').then(function (response) { $scope.acaCalUnitList = response.data; });
    $http.get('/api/ddlhelper/GetAcademicYears').then(function (response) { $scope.acaYearList = response.data; });

    $scope.submit = function (e) {
        var formElement = angular.element(e.target);
        UtilityService.RemoveFormElementValidationData(formElement);
        $scope.startDate = $('#startDate').val();
        $scope.endDate = $('#endDate').val();
        var academicSession = {
            CalendarId: $scope.acaCalendar,
            UnitId: $scope.acaCalUnit,
            YearId: $scope.acaYear,
            Sequence: $scope.sequence,
            Name: $scope.name,
            Code: $scope.code,
            StartDate: $scope.startDate,
            EndDate: $scope.endDate
        };
        $http({
            method: "POST",
            data: JSON.stringify(academicSession),
            url: '/api/admin/dashboard/AddNewStoreInItem'
        }).then(
            function mySuccess(response) {
                UtilityService.UpdateFormValidationDataOnSuccess(formElement, response);
            },
            function myError(response) {
                UtilityService.UpdateFormValidationDataOnError(formElement, response);
            });
    };
});
