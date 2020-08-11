import { MediaType } from '../enums/mediatype';

export interface BaseUserMedia {
    MediaUrl: string;
    TweetMediaType?: MediaType;
}
