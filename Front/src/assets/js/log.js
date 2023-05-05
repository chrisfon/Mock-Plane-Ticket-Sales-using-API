export default class Log{

    static async add(recordCode,action,description,detail){
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        const data = {
            userName:sessionStorage.getItem('user')||'nan',
            recordCode,
            action,
            description,
            detail : JSON.stringify(detail),
            id : new Date().getTime()
        }

        var requestOptions = {
            method: "POST",
            headers: myHeaders,
            body: JSON.stringify(data),
            redirect: "follow",
          };

        const response = await fetch("/api/Journals", requestOptions);
        console.log(response.status)
    }
}