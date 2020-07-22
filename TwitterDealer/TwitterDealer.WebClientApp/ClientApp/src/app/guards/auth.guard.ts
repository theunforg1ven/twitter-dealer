import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService,
              private router: Router,
              private toastr: ToastrService) {}

  canActivate(): boolean  {
    if (this.authService.loggedIn()) {
      return true;
    }
    this.toastr.error('No permission to access page!', 'Error', { positionClass: 'toast-bottom-right' });
    this.router.navigate(['/home']);
    return false;
  }
}
