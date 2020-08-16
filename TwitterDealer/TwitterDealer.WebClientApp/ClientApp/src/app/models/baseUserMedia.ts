import { MediaType } from '../enums/mediatype';

export interface BaseUserMedia {
    mediaUrl: string;
    tweetMediaType?: MediaType;
}
