import React from 'react';
import '../EventPost/EventPost.css';
import CommReactionBar from '../CommReactionBar/CommReactionBar';
const images = {
      like:require('../../assets/reactions/like.svg'),
      love: require('../../assets/reactions/love.svg'),
      wow: require('../../assets/reactions/wow.svg'),
      haha: require('../../assets/reactions/haha.svg'),
      angry: require('../../assets/reactions/angry.svg'),
      sad: require('../../assets/reactions/sad.svg')
};

class EventPost extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            source: null,
            id:this.props.id
        };
    }
    componentDidMount() {
        if (this.props.isPreview) {
            this.props.picture.name === 'Wybierz plik' ? this.setState({ source: "https://via.placeholder.com/750x300" })
                : this.setState({ source:URL.createObjectURL(this.props.picture)});
        }
        else {
            this.setState({ source: this.props.picture })
        }
        
    }
    render() {
        return (
            <div className="card mb-4">
                <img className="card-img-top"
                    width="750px;"
                    height="300px;"
                    src={this.state.source}
                    alt="Brak zdjęcia!" />
                <div className="card-body">
                    <h2 className="card-title">{this.props.eventName}</h2>
                    <p className="card-text showSpace">{this.props.eventDescription}</p>
                    <a href="" onClick={() => { return !this.props.isPreview}} className="btn btn-primary">Szczegóły... &rarr;</a>
                </div>
                <CommReactionBar
                    currentReaction={this.props.currentReaction}
                    id={this.props.id}
                    createdBy={this.props.createdBy}
                    reactions={this.props.reactions}
                    reactionsCount={this.props.reactionsCount}
                    date={this.props.date}
                    comments={this.props.comments}
                    commentsCount={this.props.commentsCount}
                    canLoadMore={this.props.canLoadMore}
                />
            </div>


        );
    }
}
export default EventPost;
