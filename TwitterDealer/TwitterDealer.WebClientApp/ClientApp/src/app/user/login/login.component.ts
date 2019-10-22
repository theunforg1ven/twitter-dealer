import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  formModel = {
    UserName: '',
    Password: ''
  };

  constructor(private userService: UserService) { }

  ngOnInit() {
  }
}
