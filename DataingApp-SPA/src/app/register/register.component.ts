import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // components from child to parent using output prop
  model: any = {};
  constructor( private authService: AuthService) { }

  ngOnInit() {
  }


  register(){
    this.authService.register(this.model).subscribe(() => {
      console.log('registration successful!');
    }, error =>{
      console.log(error);
    });
  }

  cancel(){
    this.cancelRegister.emit(false); // components from child to parent using output prop
    console.log('cancelled');
  }
}
