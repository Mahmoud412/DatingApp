import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any; // passing the value btween components from child to parent
  model: any = {};
  constructor() { }

  ngOnInit() {
  }


  register(){
    console.log(this.model);
  }

  cancel(){
    console.log('cancelled');
  }
}
