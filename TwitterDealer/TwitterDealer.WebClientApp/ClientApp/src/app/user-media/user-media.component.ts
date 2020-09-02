import { Component, OnInit } from '@angular/core';
import { UserMedia } from '../models/userMedia';
import { TweetsService } from '../services/tweets.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-media',
  templateUrl: './user-media.component.html',
  styleUrls: ['./user-media.component.scss']
})
export class UserMediaComponent implements OnInit {
  public link: string;
  public allMedia: UserMedia[];

  private imageWidth: number;
  private imageHeight: number;

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  onGetUserMedia(link: string): void {
    if (link == null) {
      return;
    }

    this.tweetsService.getUserMedia(link).subscribe((res: UserMedia[]) => {
      this.allMedia = null;
      this.allMedia = res;
    }, error => {
      this.toastr.error(error, 'Error ocurred', { positionClass: 'toast-bottom-right' });
      }
    );
  }

  onClear(): void {
    this.link = '';
    this.allMedia = null;
  }

  downdloadImg(href: string, name: string): void {
    const link = document.createElement('a');
    link.href = href;
    link.download = `${name}.jpg`;
    link.target = '_blank';
    link.style.display = 'none';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

}
