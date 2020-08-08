import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberListResolver implements Resolve<User[]> {

    constructor(private userService: UserService,
                private toastr: ToastrService,
                private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.getUsers().pipe(
            catchError(e => {
                this.toastr.error(e, 'Data error', { positionClass: 'toast-bottom-right' });
                this.router.navigate(['/home']);
                return of(null);
            })
        )
    }
}