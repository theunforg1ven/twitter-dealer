import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(public userService: UserService,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.userService.formModel.reset();
  }

  onSubmit() {
    this.userService.register().subscribe(
      (res: any) => {
        //debugger;
        if (res.succeeded === true) {
          this.userService.formModel.reset();
          this.toastr.success('New user created!', 'Registration successful.');
        } else {
          res.Errors.forEach((element: { Code: any; Description: string; }) => {
            switch (element.Code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is already taken', 'Registration failed.');
                break;

              default:
                this.toastr.error(element.Description, 'Registration failed.');
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
  }

}
