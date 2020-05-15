import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}; // saving out username and password

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(next => {

      console.log('logged in successfuly');

    }, error  =>  {
       console.log(error);
    });
  }

  loggedIn(){

    const token = localStorage.getItem('token'); // getting the token form localstorage

    return !!token;  // return true or false value

  }

  logout(){
    localStorage.removeItem('token');  // remove the token from the localstorage
    console.log('logged out');
  }
}
