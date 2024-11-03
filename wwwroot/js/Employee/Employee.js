$(function () {

    $("#CountryList").on('change',function () {

        var CountryId = $("#CountryList option:selected").val();

        $("#CityList").empty();
        $("#CityList").append("<option>Choose City</option>");

        $.ajax({

            type: "POST",
            url: "/Employee/GetCityDataByCountryId",
            data: { CtryId: CountryId },
            success: function (res) {
                console.log(res);
                $.each(res, function (i, e) {
                    $("#CityList").append("<option value='" + e.Id + "'>" + e.Name + "</option>");
                });
            }

        });


    });

    $("#CityList").on('change', function () {

        var CityId = $("#CityList option:selected").val();

        $("#DistrictId").empty();
        $("#DistrictId").append("<option>Choose District</option>");

        $.ajax({

            type: "POST",
            url: "/Employee/GetDistrictDataByCityId",
            data: { CtyId: CityId },
            success: function (res) {
                $.each(res, function (i, e) {
                    $("#DistrictId").append("<option value='" + e.Id + "'>" + e.Name + "</option>");
                });
            }
        });
    });
});
