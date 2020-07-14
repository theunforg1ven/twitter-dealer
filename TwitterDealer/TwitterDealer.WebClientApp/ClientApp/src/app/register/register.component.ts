import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register(): void  {
    this.authService.register(this.model).subscribe(() => {
      this.toastr.success('Registration success', 'Registration info', {
        positionClass: 'toast-bottom-right'
      });
    }, error => {
      this.toastr.error(error, 'Error ocurred', {
        positionClass: 'toast-bottom-right'
        });
      }
    );
  }

  cancel(): void  {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }
}
