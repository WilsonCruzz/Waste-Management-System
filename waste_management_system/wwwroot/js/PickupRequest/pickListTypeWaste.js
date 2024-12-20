function pickListTypeWaste() {
    $('#typeWasteID').change(function () {
        var wasteId = $('#typeWasteID').val();
        if (wasteId) {
            $.ajax({
                url: '/PickUpRequests/getVehicleByWaste',
                type: "GET",
                data: { id: wasteId },
                success: function (data) {
                    if (data.success) {
                        $('#vehicleID').val(data.description);
                    } else {
                        $('#vehicleID').val('');
                    }
                }
            });
        } else {
            $('#vehicleID').val('');
        }
    });
}