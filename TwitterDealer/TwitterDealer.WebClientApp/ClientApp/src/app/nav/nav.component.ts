import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  login(): void {
    this.authService.login(this.model).subscribe(next => {
      this.toastr.success('Welcome back', 'Logged in successfully', {
       positionClass: 'toast-bottom-right'
       });
     }, error => {
       this.toastr.error(error, 'Error ocurred', {
         positionClass: 'toast-bottom-right'
       });
     });
  }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }

  logout(): void {
    localStorage.removeItem('token');
    this.toastr.info('Logged out', 'Information', {
      positionClass: 'toast-bottom-right'
    });
  }

}
