const host = "https://provinces.open-api.vn/api/";
var callAPI = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data, "province");
        });
}
callAPI('https://provinces.open-api.vn/api/?depth=1');
var callApiDistrict = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.districts, "district");
        });
}
var callApiWard = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.wards, "ward");
        });
}

var renderData = (array, select) => {
        let row = ' <option disable value="">Vui lòng chọn</option>';
        array.forEach(element => {
            row += `<option value="${element.code}">${element.name}</option>`
        });
        document.querySelector("#" + select).innerHTML = row
    }
    (function($) {

        $("#province").change(() => {
            callApiDistrict(host + "p/" + $("#province").val() + "?depth=2");
            resetWard();
            printResult();
        });
        $("#district").change(() => {
            callApiWard(host + "d/" + $("#district").val() + "?depth=2");
            resetWard();
            printResult();
        });
        $("#ward").change(() => {
            printResult();
        })
        var resetWard = () => {
            $("#ward").val(""); // Reset giá trị phường xã về mặc định
            renderData([], "ward"); // Cập nhật danh sách phường xã
        }
        var printResult = () => {
            if ($("#district").val() != "" && $("#province").val() != "" &&
                $("#ward").val() != "") {
                let result = $("#province option:selected").text() +
                    " | " + $("#district option:selected").text() + " | " +
                    $("#ward option:selected").text();
                $("#result").text(result)
                $("#provinceValue").val($("#province option:selected").text());
                $("#districtValue").val($("#district option:selected").text());
                $("#wardValue").val($("#ward option:selected").text());
            }

        }
        $("#sumPrice").val($("#sumPriceSource").val())
        
    })(jQuery);