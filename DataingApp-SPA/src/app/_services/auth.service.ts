import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }

login(model: any){
  return this.http.post(this.baseUrl + 'login', model)
  // getting  the token from the api and store it locil and get easy acsses when we want to get it.
  .pipe(
    map((response: any ) =>{
      // inside the user it will be the token that we got from api.
      const user = response;
      if(user){
        localStorage.setItem('token', user.token);
      }
    })
  );

}
}
