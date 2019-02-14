import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public reservations: Reservation[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    //////////////////////////   (1)   ////////////////////////////

    this.fetchFromRestApi = function () {
      http.get<Reservation[]>(baseUrl + 'api/Reservations/List').subscribe(result => {
        this.reservations = result;
        this.fetchSource = "(1) Old School RestAPI";
      }, error => console.error(error));
    };

    //////////////////////////   (2)   ////////////////////////////

    this.fetchFromGrapQLClient = function () {
      http.get<Reservation[]>(baseUrl + 'api/Reservations/ListFromGraphql').subscribe(result => {
        this.reservations = result;
        this.fetchSource = "(2) .NET GraphQL Client";
      }, error => console.error(error));
    }

    //////////////////////////   (3)   ////////////////////////////

    this.fetchDirectlyFromGraphQL = function () {
      var query = `?query=
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
                    }`  ;

      http.get<Reservation[]>(baseUrl + 'graphql/' + query).subscribe(result => {
        this.reservations = result.data.reservations;
        this.fetchSource = "(3) Directly From GraphQL";
      }, error => console.error(error));
    }

    //////////////////////////////////////////////////////


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
