export const mixinResTicket = {
    methods: {

        async restickets(card,count,flight){
            let counter = 0
            for(let i = 0; i<count; i++){
                if(await this.resTicket({
                    code : new Date().getTime().toString(),
                    flight : flight.code,
                    cost : flight.cost,
                    status : '2',
                    sequence : 7

                },card)){
                    counter ++;
                }
            }
            return counter;
        },

        async resTicket(ticket,card) {
            let tck = await this.createResTicket(ticket)
            if(!tck){
                return false
            }
            const prs = {
                sequence : 6,
                BookingId : new Date().getTime().toString(),
                card,
                ticket:ticket.code,
                userName:sessionStorage.getItem('user')
            }
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");
            var raw = JSON.stringify(prs);
            var requestOptions = {
                method: "POST",
                headers: myHeaders,
                body: raw,
                redirect: "follow",
            };
            try {
                const res = await fetch("/api/Reservations", requestOptions);
                if (res.status >= 200 && res.status <= 299) {
                    return true
                } 
            } catch (e) {
                return false
            }
            return false
        },

        async createResTicket(ticket) {
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");
            var raw = JSON.stringify(ticket);
            var requestOptions = {
                method: "POST",
                headers: myHeaders,
                body: raw,
                redirect: "follow",
            };
            try {
                const res = await fetch("/api/Tickets", requestOptions);
                if (res.status >= 200 && res.status <= 299) {
                    return ticket.code
                } 
                
            } catch (e) {
                return null 
            }
            return null
        }

    }
}