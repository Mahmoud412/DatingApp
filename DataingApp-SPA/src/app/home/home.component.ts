import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;

  values: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }
  registerToggle(){

    this.registerMode = true; // setting the rigster mode to true
  }


  cancelRegisterMode(registerMode: boolean) {

    this.registerMode = registerMode; // calling the cancel method when user click on cancel
  }
}
