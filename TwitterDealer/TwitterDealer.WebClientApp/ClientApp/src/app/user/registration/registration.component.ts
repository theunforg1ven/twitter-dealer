import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(public userService: UserService) { }

  ngOnInit() {
    this.userService.formModel.reset();
  }

  async onSubmit() {
     await this.userService.register().then(
      (res: any) => {
        debugger;
        if (res.Succeeded === true) {
          this.userService.formModel.reset();
          //this.toastr.success('New user created!', 'Registration successful.');
        } else {
          res.Errors.forEach((element: { Code: any; Description: string; }) => {
            switch (element.Code) {
              case 'DuplicateUserName':
                //this.toastr.error('Username is already taken','Registration failed.');
                break;
              default:
              //this.toastr.error(element.Description,'Registration failed.');
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
