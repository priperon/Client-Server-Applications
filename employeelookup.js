$(function () {

    $("#getbutton").click(async (e) => { // Click event handler makes aysynchronous fetch to server
        try {
            let email = $("#TextBoxLastname").val();
            $("#status").text("please wait...");
            let response = await fetch(`api/employee/${email}`);
            if (response.ok) {
                let data = await response.json(); // This returns a promise, so we await it
                if (data.email !== "not found") {
                    $("#lastname").text(data.lastname);
                    $("#title").text(data.title);
                    $("#firstname").text(data.firstname);
                    $("#phone").text(data.phoneno);
                    $("#status").text("employee found");
                } else {
                    $("#firstname").text("not found");
                    $("#lastname").text("");
                    $("#title").text("");
                    $("#phone").text("");
                    $("#status").text("not such employee");
                }
            } else if (response.status !== 404) { // Probably some other client side error
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else { // else 404 not found
                $("#status").text("no such path on server");
            } // else
        } catch (error) { // Catastrophic
            $("#status").text(error.message);
        } // try/catch

    }); // click event

}); // jQuery ready method

// Server was reached but server had a problem with the call
const errorRtn = (problemJson, status) => {
    if (status > 499) {
        $("#status").text("Problem server side, see debug console")
    } else {
        let kyes = Object.kyes(problemJson.errors)
        problem = {
            status: status,
            statusText: problemJson.errors[keys[0]][0], // first error
        };
        $("#status").text("Problem client side, see browser console");
        console.log(problem);
    } // else
}