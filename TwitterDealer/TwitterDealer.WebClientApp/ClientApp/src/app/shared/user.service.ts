import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder) { }

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
  comparePasswords(formBuilder: FormGroup){
    const confirmPassword = formBuilder.get('ConfirmPassword');
    if (confirmPassword.errors == null || 'passwordMismatch' in confirmPassword.errors) {
      if (formBuilder.get('Password').value !== confirmPassword.value) {
        confirmPassword.setErrors({ passwordMismatch: true });
      } else {
        confirmPassword.setErrors(null);
      }
    }
  }
}
