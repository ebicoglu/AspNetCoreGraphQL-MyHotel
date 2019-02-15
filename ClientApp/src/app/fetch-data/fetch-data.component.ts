import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public reservations: Reservation[];
  http: HttpClient;
  baseUrl: string;
  fetchSource: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }




  //////////////////////////   (1)   ////////////////////////////
  fetchFromRestApi(): any {
    this.http.get<Reservation[]>(this.baseUrl + 'api/Reservations/List').subscribe(result => {
      this.reservations = result;
      this.fetchSource = "(1) Old School RestAPI";
    }, error => console.error(error));
  }

  //////////////////////////   (2)   ////////////////////////////
  fetchFromGraphQlClient(): any {
    this.http.get<Reservation[]>(this.baseUrl + 'api/Reservations/ListFromGraphql').subscribe(result => {
      this.reservations = result;
      this.fetchSource = "(2) .NET GraphQL Client";
    },
      error => console.error(error));
  }

  //////////////////////////   (3)   ////////////////////////////
  fetchDirectlyFromGraphQl(): any {
    const query = `?query=
                    {
                      reservations {
                        checkinDate
                        guest  {
                          name
                        }
                        room {
                          name
                        }
                      }
                    }`;

    this.http.get<any>(this.baseUrl + 'graphql/' + query).subscribe(result => {
      this.reservations = result.data.reservations;
      this.fetchSource = "(3) Directly From GraphQL";
    },
      error => console.error(error));
  }


  //////////////////////////   (4)   ////////////////////////////
    fetchUsingVanillaJs(): any {
    fetch('/graphql', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
        },
        body: JSON.stringify({
          query: 'query reservation {reservations {checkinDate guest  {name} room {name}}}'
        })
      })
      .then(r => r.json())
      .then(response => {
        this.reservations = response.data.reservations;
        this.fetchSource = "(4) Using Vanilla JS";
      });
  }

  //////////////////////////   (5)   ////////////////////////////
  fetchUsingApolloClient(): any {
     
    var client = new Apollo.lib.ApolloClient(
      {
        networkInterface: Apollo.lib.createNetworkInterface({
          uri: "https://localhost:44349/graphql"
        })
      });

    const query = Apollo.gql`
query reservation {
  reservations {
    checkinDate
    guest  {
      name
    }
    room {
      name
    }
  }
}`;
     
    client.query({
      query: query
    }).then(result => {
      this.reservations = result.data.reservations;
      this.fetchSource = "(5) Using Apollo Client";
    });

  }

}

interface Room {
  id: number;
  number: number;
  name: string;
  status: string;
  allowedSmoking: boolean;
}

interface Guest {
  id: number;
  name: string;
  registerDate: Date;
}

interface Reservation {
  checkinDate: Date;
  checkoutDate: Date;
  room: Room;
  guest: Guest;
}

