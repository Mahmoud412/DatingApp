import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-memebr-edit',
  templateUrl: './memebr-edit.component.html',
  styleUrls: ['./memebr-edit.component.css']
})
export class MemebrEditComponent implements OnInit {
   @ViewChild('editFrom') editFrom: NgForm;
   user: User;
   @HostListener('window:beforeunload', ['$event'])
   unloadNotification($event: any){
      if (this.editFrom.dirty)
      {
        $event.returnValue = true;
      }
   }
  constructor(private route: ActivatedRoute, private alertifyService: AlertifyService,
              private userService: UserService , private authService: AuthService) { }

  ngOnInit() {

    this.route.data.subscribe(data => {
      this.user = data.user;
    });
  }

  updateUser(editFrom){
    this.userService.updateUser(this.authService.decodedToken.nameid , this.user).subscribe(next => {
      this.alertifyService.success('profile update successfully');
      editFrom.reset(this.user);
    }, error => {
      this.alertifyService.error(error);
    });


  }

}
