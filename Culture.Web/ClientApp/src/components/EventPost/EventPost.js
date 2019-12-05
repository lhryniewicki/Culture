import React from 'react';
import { Link } from 'react-router-dom';
import '../EventPost/EventPost.css';
import CommReactionBar from '../CommReactionBar/CommReactionBar';
import { isAdmin } from '../../utils/JwtUtils';

class EventPost extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            source: null
        };

    }

    changeFile = () => {
        const file = this.props.picture;
        const reader = new FileReader();
        const url = reader.readAsDataURL(file);

        reader.onloadend = function (e) {
            this.setState({
                source: [reader.result]
            })
        }.bind(this);

    }
   

    componentDidMount() {
        if (this.props.isPreview) {
            
            this.props.picture.name === 'Wybierz plik' ? this.setState({ source: "https://via.placeholder.com/750x300" })
                : this.changeFile()
        }
        else {
            
            this.setState({ source: this.props.picture });
        }
        
    }

    componentDidUpdate(prevProps,prevState) {
        if (this.state.source !== 'https://via.placeholder.com/750x300' && prevState.source !== this.state.source) {
            this.setState({ source: this.state.source });
        }

        if (this.state.source !== this.props.picture) {
            this.setState({ source: this.props.picture });

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
                <div className="card-body myCardBody">
                    <h2 className="card-title mt-0">{this.props.eventName}</h2>
                    <p className="card-text showSpace">{this.props.eventDescription}</p>
                    {this.props.isPreview === false ?
                        <Link to={`/wydarzenie/szczegoly/${this.props.urlSlug}`}><span className="btn btn-primary">Szczegóły... &rarr;</span></Link>
                        :
                        <span className="btn btn-primary">Szczegóły... &rarr;</span>
                    }
                    {
                        isAdmin() ?
                            <div className="float-right">
                                <i className="fas fa-trash-alt fa-lg" onClick={this.props.isPreview === false ? (e) => this.props.deleteEvent(e, this.props.id) : null} />
                            </div>
                            :
                            null
                    }
                </div>
                <CommReactionBar
                    avatarPath={this.props.avatarPath}
                    imageClick={this.props.imageClick}
                    currentReaction={this.props.currentReaction}
                    id={this.props.id}
                    createdBy={this.props.createdBy}
                    reactions={this.props.reactions}
                    reactionsCount={this.props.reactionsCount}
                    date={this.props.isPreview === false ? this.props.date : new Date().toLocaleDateString("en-Us")}
                    comments={this.props.comments}
                    commentsCount={this.props.commentsCount}
                    canLoadMore={this.props.canLoadMore}
                    isPreview={this.props.isPreview}
                    createdById={this.props.createdById}
                />
            </div>


        );
    }
}
export default EventPost;
