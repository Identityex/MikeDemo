$(document).ready(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();

    GetVehicles();
    ToggleArea("Truck");
    $('#typeSel').change(function () { ToggleArea($('#typeSel').val()); });
    $('#addBtn').click(function () { AddVehicle(); });


    //Change what is visable
    function ToggleArea(type) {
        switch (type) {
            case "Truck":
                $('#truckArea').show();
                $('#carArea').hide();
                $('#motorbikeArea').hide();
                break;

            case "Car":
                $('#truckArea').hide();
                $('#carArea').show();
                $('#motorbikeArea').hide();
                break;

            case "Motorbike":
                $('#truckArea').hide();
                $('#carArea').hide();
                $('#motorbikeArea').show();
                break;
        }
    }

    //Ajax Call to create a new vehicle
    function AddVehicle() {
        let vehicle = new Object();
        let type = $('#typeSel').val();

        let price = $('#priceInput').val();

        if (isNaN(price)) {
            //Show Toast or Validation on error
            return;
        }


        vehicle.name = $('#nameInput').val();
        vehicle.colour = $('#colourInput').val();
        vehicle.price = price;

        if (type === "Truck") {

            let boxSize = $('#boxSizeInput').val();
            if (isNaN(boxSize)) {
                //Show Toast or Validation on error
                return;
            }

            vehicle.boxSize = boxSize;
            vehicle.fourByFour = $('#4X4Input').prop('checked');
        }
        else if (type === "Car") {
            let doors = $('#doorsInput').val();
            if (isNaN(doors)) {
                //Show Toast or Validation on error
                return;
            }

            vehicle.doors = doors;
        }
        else if (type === "Motorbike") {
            vehicle.passengerSeat = $('#passSeatInput').prop('checked');
        }
        else {
            //Show Toast or Validation on error
            return;
        }

        $.ajax({
            type: "Post",
            url: '/Automobile/AddVehicle',
            dataType: 'html',
            data: { vehicle: JSON.stringify(vehicle), type: type },
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", token);
            },
            success: function () {
                GetVehicles();
            },
            error: function (ex) { },
            complete: function () {
            }
        });

    }


    //Ajax to Delete Vehicle 
    function DeleteVehicle(id) {
        $.ajax({
            type: "Delete",
            url: '/Automobile/DeleteVehicle',
            data: { id: id },
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", token);
            },
            success: function (data) {
                GetVehicles();
            },
            error: function (ex) { },
            complete: function () {
            }
        });
    }

    //Ajax to get vehicles
    function GetVehicles() {
        $.ajax({
            type: "Get",
            url: '/Automobile/GetVehicles',
            dataType: 'json',
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", token);
            },
            success: function (data) {
                $('#AutomobileTable').find('tbody').empty();
                if (data.length == 0) {

                }
                else {
                    $.each(data, function (index, item) {
                        var newRow =
                            `<tr>
                                <td>${item.name}</td>
                                <td>${item.colour}</td>
                                <td>${item.price}</td>
                                <td><button class='delete-btn' value='${item.automobileId}'>Delete</button></td>
                            </tr>`;

                        $('#AutomobileTable').find('tbody').append(newRow);
                    });
                }
                $('.delete-btn').click(function () { DeleteVehicle($(this).val()); });
            },
            error: function (ex) { },
            complete: function () {
            }
        });
    }
});