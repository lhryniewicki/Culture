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
        };
    }
    componentDidMount() {

    }
    render() {
        return (
            <div className="card mb-4">
                {console.log(this.props.picture)}
                <img className="card-img-top"
                    width="750px;"
                    height="300px;"
                    src={this.props.picture.name === 'Wybierz plik' ? "https://via.placeholder.com/750x300" : URL.createObjectURL(this.props.picture)}
                    alt="Brak zdjęcia!" />
                <div className="card-body">
                    <h2 className="card-title">{this.props.eventName}</h2>
                    <p className="card-text showSpace">{this.props.eventDescription}</p>
                    <a href="" onClick={() => { return !this.props.isPreview}} className="btn btn-primary">Szczegóły... &rarr;</a>
                </div>
                <CommReactionBar date={this.props.date}/>
            </div>


        );
    }
}
export default EventPost;
