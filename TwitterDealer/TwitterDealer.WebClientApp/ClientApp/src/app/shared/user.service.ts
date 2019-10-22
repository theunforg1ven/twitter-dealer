import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rootUrl = 'https://localhost:44320/api';

  constructor(private fb: FormBuilder,
              private http: HttpClient) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    TwitterUsername: ['', Validators.required],
    Email: ['', Validators.email],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(5)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords})
  });

  // condition of error: confirmPassword.errors = {passwordMissmatch:true}
  comparePasswords(formBuilder: FormGroup) {
    const confirmPassword = formBuilder.get('ConfirmPassword');
    if (confirmPassword.errors == null || 'passwordMismatch' in confirmPassword.errors) {
      if (formBuilder.get('Password').value !== confirmPassword.value) {
        confirmPassword.setErrors({ passwordMismatch: true });
      } else {
        confirmPassword.setErrors(null);
      }
    }
  }

  register() {
    //debugger;
    const body = {
      UserName: this.formModel.value.UserName,
      TwitterUsername: this.formModel.value.TwitterUsername,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.rootUrl + '/applicationuser/register', body);
  }

  login(formData) {
    return this.http.post(this.rootUrl + '/applicationuser/login', formData);
  }

  getUserProfile() {
    return this.http.get(this.rootUrl + '/userprofile');
  }

}
